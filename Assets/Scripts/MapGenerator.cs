using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapGenerator : MonoBehaviour {

	public GameObject wall;
	public GameObject walkable;
	public GameObject destination;

	public int worldSize = 33;
	public float tileSize = 0.32f;
	public float tileScale = 4.0f;
	public float density = .75f;
	public float complexity = .75f;

	public Vector2 startPosition = new Vector2(1, 1);

	public List<Vector3> nodes;

	void Display(int[,] world) {
		bool createDest = false;
		for (int i = 0; i < worldSize; ++i) {
			for (int j = 0; j < worldSize; ++j) {
				GameObject inst;
				if (world[i,j] == 1) {
					inst = Instantiate(wall) as GameObject;
				} else if (world[i,j] == 0) {
					inst = Instantiate(walkable) as GameObject;
				} else {
					inst = Instantiate(destination) as GameObject;
					createDest = true;
				}
				inst.transform.parent = transform;
				inst.transform.position = new Vector3(i * tileSize * tileScale, j * tileSize * tileScale,
				                                      	-0.5f * tileSize * tileScale * inst.transform.localScale.z);
				inst.transform.localScale *= tileScale;
				if (createDest) {
					GameObject.Find("DirectionArrow").GetComponent<ArrowController>().destinationPosition = inst.transform;
					createDest = false;
				}
			}
		}
	}

	int[,] Maze(int width, int height, float complexity, float density) {
		
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
		return Z;
	}

	List<Vector2> getNeighbours(Vector2 current, int[,] maze) {
		List<Vector2> res = new List<Vector2>();
		if (maze[(int)(current.x + 1), (int)current.y] == 0) {
			res.Add(new Vector3(current.x+1, current.y));
		}
		if (maze[(int)(current.x - 1), (int)current.y] == 0) {
			res.Add(new Vector3(current.x-1, current.y));
		}
		if (maze[(int)current.x, (int)(current.y+1)] == 0) {
			res.Add(new Vector3(current.x, current.y+1));
		}
		if (maze[(int)current.x, (int)(current.y-1)] == 0) {
			res.Add(new Vector3(current.x, current.y-1));
		}
		return res;
	}

	int[,] dist;

	void BFS(Vector2 start, int[,] maze) {
		List<Vector2> q = new List<Vector2>();
		dist = new int[worldSize, worldSize];

		q.Add(start);

		while(q.Count > 0) {

			Vector2 node = q[0];
			int rmInd = 0;
			for (int i = 1; i < q.Count; ++i) {
				if (dist[(int)q[i].x, (int)q[i].y] < dist[(int)node.x, (int)node.y]) {
					node = q[i];
					rmInd = i;
				}
			}
			q.RemoveAt(rmInd);

			foreach (Vector2 child in getNeighbours(node, maze)) {
				int alt = dist[(int)node.x, (int)node.y] + 1;

				int childDist = dist[(int)child.x, (int)child.y];

				if (childDist == 0) {
					dist[(int)child.x, (int)child.y] = alt;
					q.Add(child);
				} else if (q.Contains(child) && alt < childDist) {
					dist[(int)child.x, (int)child.y] = alt;
				}
			}

		}
	}

	void PositionPlayer(Vector2 position) {
		GameObject player = GameObject.Find("Player");
		player.transform.position = new Vector3(position.x, position.y, player.transform.position.z);
	}

	void PositionSpawner() {
		GameObject spawner = GameObject.Find("_Spawner");
		spawner.GetComponent<CEnemySpawner>().mBounds = new Bounds(new Vector3((worldSize*tileSize*tileScale)/2, (worldSize*tileSize*tileScale)/2, 0), 
		                                                           new Vector3(worldSize*tileSize*tileScale, worldSize*tileSize*tileScale));
	}
	
	// Use this for initialization
	void Start () 
	{
		int[,] maze = Maze(worldSize, worldSize, complexity, density);
		BFS(startPosition, maze);

		Vector3 tempMax = new Vector3(0,0,0);
		for (int i=0; i < worldSize; ++i) {
			for (int j=0; j < worldSize; ++j) {
				int t = dist[i, j];
				if (t > (int)tempMax.z) {
					tempMax.x = i;
					tempMax.y = j;
					tempMax.z = t;
				}
			}
		}

		maze[(int)tempMax.x, (int)tempMax.y] = 3;
		Display(maze);
		PositionPlayer(startPosition);
		PositionSpawner();

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


}