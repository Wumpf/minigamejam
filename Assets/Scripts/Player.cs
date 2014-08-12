using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public Transform wheel;
	
	const uint maxNumPlayers = 5;
	const float wheelBodyDistance = 0.8f;

	uint numPlayers = 0;

	List<PlayerController> wheelController = new List<PlayerController>();
	GamepadPlayerController lightController;

	// Use this for initialization
	void Start ()
	{
		rigidbody2D.fixedAngle = false; // Workaround for missing torque (was intentially set to true before)

		numPlayers = System.Math.Min(maxNumPlayers, (uint)Input.GetJoystickNames().Length);
		// TODO: 3 player not allowed

		lightController = GameObject.Find("Spot").AddComponent<GamepadPlayerController>();
		lightController.JoystickIndex = 0;

		float angleStep = Mathf.PI * 2 / (numPlayers-1);
		float currentAngle = 0.0f;
		for(uint i=1; i<numPlayers; ++i)
		{
			var wheelInstance = (Transform)Instantiate(wheel);
			wheelInstance.parent = transform;
			wheelInstance.position = transform.position + 
									 new Vector3(Mathf.Sin(currentAngle), Mathf.Cos(currentAngle), 0.0f) * wheelBodyDistance;

			GamepadPlayerController player = wheelInstance.gameObject.AddComponent<GamepadPlayerController>();
			player.JoystickIndex = i;
			wheelController.Add(player);

			currentAngle += angleStep;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach(var controller in wheelController)
		{
			rigidbody2D.AddForceAtPosition(controller.Direction * controller.Speed, controller.transform.position);
		}
	}
}
