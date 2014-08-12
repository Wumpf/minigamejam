using UnityEngine;

public class GamepadPlayerController : PlayerController
{
	public uint JoystickIndex = 0;

	// Use this for initialization
	public override void Start ()
	{
		Direction = Vector2.up;
		Speed = 0.0f;
	}
	
	// Update is called once per frame
	public override void Update()
	{
		Vector2 newDir = new Vector2(Input.GetAxis("DirX" + JoystickIndex),
		              	             Input.GetAxis("DirY" + JoystickIndex));
		if(newDir.sqrMagnitude > 0.5f)
			Direction = newDir;
		/*else
		{
			Vector3 lookDir = transform.TransformDirection(transform.forward);
			Direction = new Vector2(lookDir.x, lookDir.y);
		}*/
		Speed = Input.GetAxis("Action" + JoystickIndex);

		base.Update();
	}
}
