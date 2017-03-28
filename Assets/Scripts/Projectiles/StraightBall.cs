using UnityEngine;
using System.Collections;

public class StraightBall : BaseProjectile {

	public float forwardSpeed;
	public float spinSpeed;

	protected override void Start(){

		base.Start ();
		GetComponent<Rigidbody> ().AddForce (transform.forward * forwardSpeed);
	}

	protected override void Update(){

		base.Update ();

		if (isActive) {

			// Move
			transform.position += transform.forward * forwardSpeed * Time.deltaTime;

			// Spin
			GetComponentInChildren<Transform>().Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
		}
	}
}