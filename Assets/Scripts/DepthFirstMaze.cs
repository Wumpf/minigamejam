using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DepthFirstMaze : MonoBehaviour {


	public int worldSize = 33;
	public Vector2 exit = new Vector2(0,0);

	public GameObject wall;
	public GameObject walkable;
	public float tileSize = 0.32f;
	public float tileScale = 4.0f;

	int[,] world;

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
				inst.transform.position = new Vector3(i * tileSize * tileScale, j * tileSize * tileScale, 0);
				inst.transform.localScale *= tileScale;
			}
		}
	}

	void generateMaze(int xInp, int yInp) {
		
		List<Vector2> neighbours = new List<Vector2>();
		int x = xInp;
		int y = yInp;

		world[x, y] = 1;
		
		if (x > 1) { 
			neighbours.Add(new Vector2(y, x - 2)); 
		};
		if (x < worldSize - 2) { 
			neighbours.Add(new Vector2(y, x + 2)); 
		};
		if (y > 1) { 
			neighbours.Add(new Vector2(y - 2, x)); 
		};
		if (y < worldSize - 2) { 
			neighbours.Add(new Vector2(y + 2, x)); 
		};

		while (neighbours.Count > 0) {

			world[x, y] = 1;

			if (x > 1) { 
				neighbours.Add(new Vector2(y, x - 2)); 
			};
			if (x < worldSize - 2) { 
				neighbours.Add(new Vector2(y, x + 2)); 
			};
			if (y > 1) { 
				neighbours.Add(new Vector2(y - 2, x)); 
			};
			if (y < worldSize - 2) { 
				neighbours.Add(new Vector2(y + 2, x)); 
			};

			int x_ = (int)neighbours[(int)Random.Range(0, neighbours.Count)].x;
			int y_ = (int)neighbours[(int)Random.Range(0, neighbours.Count)].y;
			
			if (world[x_, y_] == 0) {
				if (x_ > x) {
					world[x+1, y] = 1;
				}
				if (x_ < x) {
					world[x-1, y] = 1;
				}
				if (y_ > y) {
					world[x, y+1] = 1;
				}
				if (y_ < y) {
					world[x, y-1] = 1;
				}
			}
			x = x_;
			y = y_;

		}
	}


	// Use this for initialization
	void Start () {

		world = new int[worldSize, worldSize];
		generateMaze((int)exit.x, (int)exit.y);

	}

	// Update is called once per frame
	void Update () {
	
	}
}
