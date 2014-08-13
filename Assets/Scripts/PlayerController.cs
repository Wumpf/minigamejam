using UnityEngine;
using System.Collections;

abstract public class PlayerController : MonoBehaviour
{
	private Quaternion baseRotation;

	// Use this for initialization
	public virtual void Start ()
	{
		baseRotation = transform.rotation;
	}
	
	// Update is called once per frame
	public virtual void Update()
	{
		Direction.Normalize();
		float angle = Mathf.Atan2(Direction.x, -Direction.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)) * baseRotation;
		
		ParticleSystem particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
		if(particleSystem != null)
			particleSystem.enableEmission = Speed > 0;
	}


	public Vector2 Direction { get; protected set; }
	public float Speed { get; protected set; }
}
