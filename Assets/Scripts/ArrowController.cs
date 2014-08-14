using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

	public Transform destinationPosition;
	
	// Use this for initialization
	void Start () {
		//destinationPosition = GameObject.Find("MapGenerator").GetComponent<MapGenerator>().tempMax;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cameraTransform = GameObject.Find("Player").transform.position;
		this.transform.position = new Vector3(cameraTransform.x, cameraTransform.y, this.transform.position.z);

		Vector3 direction = destinationPosition.position - this.transform.position;
		direction.Normalize();
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}
}
