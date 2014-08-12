using UnityEngine;
using System.Collections;

abstract public class PlayerController : MonoBehaviour
{
	// Use this for initialization
	public abstract void Start ();
	
	// Update is called once per frame
	public virtual void Update()
	{
		Direction.Normalize();
		float angle = Mathf.Atan2(-Direction.x, Direction.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}


	public Vector2 Direction { get; protected set; }
	public float Speed { get; protected set; }
}
