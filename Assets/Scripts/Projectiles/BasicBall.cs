using UnityEngine;
using System.Collections;

public class BasicBall : BaseProjectile {

	public float forwardSpeed;
	public float spinSpeed;

	protected override void Start(){

		base.Start();
		GetComponent<Rigidbody>().AddForce (transform.forward * forwardSpeed);

		transform.Rotate(0, 0, 90);
	}

	protected override void Update(){

		base.Update ();

		Vector3 movementDirection = Vector3.Normalize(target.position - transform.position);

		if (isActive){
			transform.position += movementDirection * forwardSpeed * Time.deltaTime;
			transform.Rotate(0, 0, spinSpeed);
		}
	}
}