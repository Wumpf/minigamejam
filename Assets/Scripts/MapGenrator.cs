using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenrator : MonoBehaviour {

	public GameObject wall;
	public GameObject walkable;
	public int worldSize = 33;

	int[,] world;
	List<Wall> wallList;
	
	// Use this for initialization
	void Start () 
	{
		world = new int[worldSize, worldSize];
		wallList = new List<Wall>();

		int i = 1;
		int j = 1;

		world[i,j] = 1;
		addNeighboringCells(i,j);

		while(!(wallList.Count == 0)) {

			killTheOnot(wallList[(int)(Random.value * wallList.Count)]);

		}
		display();
	}

	void killTheOnot(Wall w) {
		int wli = w.i;
		int wlj = w.j;
		
		if (wli > 32) {
			wli = 32;
		}
		if (wlj > 32) {
			wlj = 32;
		}
		if (wli < 0) {
			wli = 0;
		}
		if (wlj < 0) {
			wlj = 0;
		}
		
		if(world[wli, wlj] == 0) {
			if (wli >= 1)
				world[wli-1, wlj] = 1;
			
			world[wli, wlj] = 1;
			addNeighboringCells(wli-1, wlj);
		} else {
//			wallList.Remove(wallList[r]);
		}

	}

	void display() {
		for (int i = 0; i < worldSize; ++i) {
			for (int j = 0; j < worldSize; ++j) {
				if (world[i,j] == 0) {
					GameObject instWall = Instantiate(wall) as GameObject;
					instWall.transform.position = new Vector3(i * 0.32f, j * 0.32f, 0);
				} else {
					GameObject instWalk = Instantiate(walkable) as GameObject;
					instWalk.transform.position = new Vector3(i * 0.32f, j * 0.32f, 0);
				}
			}
		}
	}

	void addNeighboringCells(int i, int j) 
	{
		//left
		if (i >= 1 && j >= 1)
			wallList.Add(new Wall(i-1,j-1));
		else
			wallList.Add(new Wall(i,j));

		if (i >= 1)
			wallList.Add(new Wall(i-1,j));
		else
			wallList.Add(new Wall(i,j));

		if (i >= 1)
			wallList.Add(new Wall(i-1,j+1));
		else
			wallList.Add(new Wall(i,j+1));

		//middle
		if (j >= 1)
			wallList.Add(new Wall(i,j-1));
		else
			wallList.Add(new Wall(i,j));

		wallList.Add(new Wall(i,j+1));

		//right
		if (j >= 1)
			wallList.Add(new Wall(i+1,j-1));
		else
			wallList.Add(new Wall(i+1,j));

		wallList.Add(new Wall(i+1,j));
		wallList.Add(new Wall(i+1,j+1));
	}

	void Maze(int width=81, int height=51, float complexity=.75f, float density=.75f) {

		//only odd shapes
		Vector2 shape = new Vector2((height / 2) * 2 + 1, (width / 2) * 2 + 1);

		// Adjust complexity and density relative to maze size
		complexity = (int) (complexity * (5 * (shape[0] + shape[1])));
		density    = (int) (density * (shape[0] / 2 * shape[1] / 2));

		// Build actual maze           
		int[,] Z;
		Z = new int[width, height];

		// Fill borders
		Z[0, 0] = 1;
		//Z[-1, 0] = 1;
		Z[0, 0] = 1;
		//Z[0, -1] = 1;
		
		// Make aisles
		/*
		for (float i = 0; i < density; ++i) {
			float x = rand(0, shape[1] / 2) * 2; 
			float y = rand(0, shape[0] / 2) * 2;
			Z[y, x] = 1;
			for (float j = 0; j < complexity; ++j) {
				int[] neighbours = new int[];
				if (x > 1) {             neighbours.append((y, x - 2))};
				if x < shape[1] - 2:  neighbours.append((y, x + 2));
				if y > 1:             neighbours.append((y - 2, x));
				if y < shape[0] - 2:  neighbours.append((y + 2, x));
				if len(neighbours) {
					y_,x_ = neighbours[rand(0, len(neighbours) - 1)];
					if Z[y_, x_] == 0 {
						Z[y_, x_] = 1;
						Z[y_ + (y - y_) // 2, x_ + (x - x_) // 2] = 1;
						x, y = x_, y_;
					}				  
				}
			}
		}
		*/
	}


	// Update is called once per frame
	void Update () 
	{
		
	}


}