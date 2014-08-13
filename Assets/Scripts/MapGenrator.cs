using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenrator : MonoBehaviour {

	public GameObject wall;
	public GameObject walkable;
	public int worldSize = 33;
	public float tileSize = 0.32f;
	public float tileScale = 4.0f;
	public float density = .75f;
	public float complexity = .75f;
	
	void display(int[,] world) {
		for (int i = 0; i < worldSize; ++i) {
			for (int j = 0; j < worldSize; ++j) {
				GameObject inst;
				if (world[i,j] == 0) {
					inst = Instantiate(wall) as GameObject;
				} else {
					inst = Instantiate(walkable) as GameObject;
				}
				inst.transform.parent = transform;
				inst.transform.position = new Vector3(i * tileSize * tileScale, j * tileSize * tileScale,
				                                      	-0.5f * tileSize * tileScale * inst.transform.localScale.z);
				inst.transform.localScale *= tileScale;
			}
		}
	}

	void Maze(int width, int height, float complexity, float density) {
		
		//only odd shapes
		Vector2 shape = new Vector2((height / 2) * 2 + 1, (width / 2) * 2 + 1);
		
		// Adjust complexity and density relative to maze size
		complexity = (int)(complexity * (5 * (shape[0] + shape[1])));
		density    = (int)(density * (shape[0] / 2 * shape[1] / 2));
		
		// Build actual maze           
		int[,] Z;
		Z = new int[width, height];
		
		// Fill borders
		for (int x = 0; x < width; ++x) {
			Z[0, x] = 1;
			Z[x, 0] = 1;
			Z[width-1, x] = 1;
			Z[x, height-1] = 1;
		}

		// Make aisles
		for (int i = 0; i < (int)density; ++i) {
			int x = (int)Random.Range(0, shape[1] / 2) * 2; 
			int y = (int)Random.Range(0, shape[0] / 2) * 2;
			Z[y, x] = 1;
			for (int j = 0; j < (int)complexity; ++j) {
				List<Vector2> neighbours = new List<Vector2>();

				if (x > 1) { 
					neighbours.Add(new Vector2(y, x - 2)); 
				};
				if (x < shape[1] - 2) { 
					neighbours.Add(new Vector2(y, x + 2)); 
				};
				if (y > 1) { 
					neighbours.Add(new Vector2(y - 2, x)); 
				};
				if (y < shape[0] - 2) { 
					neighbours.Add(new Vector2(y + 2, x)); 
				};

				if (neighbours.Count >= 0) {
					int y_ = (int)neighbours[(int)Random.Range(0, neighbours.Count - 1)].x;
					int x_ = (int)neighbours[(int)Random.Range(0, neighbours.Count - 1)].y;
					if (Z[y_, x_] == 0) {
						Z[y_, x_] = 1;
						if (x_ > x) {
							Z[x+1, y] = 1;
						}
						if (x_ < x) {
							Z[x-1, y] = 1;
						}
						if (y_ > y) {
							Z[x, y+1] = 1;
						}
						if (y_ < y) {
							Z[x, y-1] = 1;
						}
						x = x_;
						y = y_;
					}		  
				}
			}
		}
		display(Z);
	}
	
	// Use this for initialization
	void Start () 
	{
		Maze(worldSize, worldSize, complexity, density);
	}

	
	// Update is called once per frame
	void Update () 
	{
		
	}


}