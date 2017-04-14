using UnityEngine;
using System.Collections;

public class StraightBall : BaseProjectile {

	public float forwardSpeed;

	protected override void Start(){

		base.Start ();
		GetComponent<Rigidbody> ().AddForce (transform.forward * forwardSpeed);
	}

	protected override void Update(){

		base.Update ();

		if (isActive) {
			transform.position += transform.forward * forwardSpeed * Time.deltaTime;
		}
	}
}