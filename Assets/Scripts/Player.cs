using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public Transform wheel;
	
	const uint maxNumPlayers = 5;
	const float wheelBodyDistance = 0.8f;

	uint numPlayers = 0;

	List<PlayerController> playerController = new List<PlayerController>();

	// Use this for initialization
	void Start ()
	{
		numPlayers = System.Math.Min(maxNumPlayers, (uint)Input.GetJoystickNames().Length);
		// TODO: 3 player not allowed

		float angleStep = Mathf.PI * 2 / numPlayers;
		float currentAngle = 0.0f;

		for(uint i=0; i<numPlayers; ++i)
		{
			var wheelInstance = (Transform)Instantiate(wheel);
			wheelInstance.parent = transform;
			wheelInstance.position = transform.position + 
									 new Vector3(Mathf.Sin(currentAngle), Mathf.Cos(currentAngle), 0.0f) * wheelBodyDistance;

			GamepadPlayerController player = wheelInstance.gameObject.AddComponent<GamepadPlayerController>();
			player.JoystickIndex = i;
			playerController.Add(player);

			currentAngle += angleStep;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach(var controller in playerController)
		{
			rigidbody2D.AddForceAtPosition(controller.Direction * controller.Speed, controller.transform.position);
		}
	}
}
