using UnityEngine;
using System.Collections;

public class CEnemySpawner : MonoBehaviour {
	
	float mNextSpawnTime;
	public float mSpwanTime =100;
	
	public Bounds mBounds = new Bounds(Vector3.zero, new Vector3(5,5,0));
	
	// Use this for initialization
	void Start ()
	{
		Debug.Log (Input.GetJoystickNames ().GetLength (0));
	}
	
	void Spawner(){
		int i = 0;
		GameObject prefab = GameSingelton.instance.mEnemy[i];

		GameObject instance = GameObject.Instantiate (prefab) as GameObject;
		
		Vector3 position = mBounds.center + Vector3.Scale (mBounds.size, new Vector3 (Random.value, Random.value, Random.value)) 
							- mBounds.size * 0.5f;
		
		instance.transform.position = position;
	}
	// Update is called once per frame
	void Update ()
	{
		if (mNextSpawnTime <= 0) {
			//spawn
			Debug.Log ("Spawn");
			mNextSpawnTime = mSpwanTime;
			Spawner();
		}
		mNextSpawnTime -= Time.deltaTime;
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (mBounds.center, mBounds.size);
	}
}
