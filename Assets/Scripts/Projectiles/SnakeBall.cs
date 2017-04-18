using UnityEngine;
using System.Collections;

public class SnakeBall : BaseProjectile {

	public float forwardSpeed;
	public float sidewaysSpeed;
	public float frequency;

	private float initTime;
	private Transform child;

	protected override void Start ()
	{
		base.Start();
		initTime = Time.timeSinceLevelLoad;

		GetComponent<Rigidbody>().AddForce (transform.forward * forwardSpeed);
		child = GetComponentInChildren<Transform> ();
	}
	
	protected override void Update (){

		base.Update ();
		float lifetime = Time.timeSinceLevelLoad - initTime;

		Vector3 old = transform.position;

		if (isActive) {
			transform.position += transform.forward * forwardSpeed * Time.deltaTime;
			transform.position += transform.right * sidewaysSpeed * Mathf.Cos (lifetime * frequency) * Time.deltaTime;
		}

	}
}