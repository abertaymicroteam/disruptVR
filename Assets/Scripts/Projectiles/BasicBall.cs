using UnityEngine;
using System.Collections;

public class BasicBall : BaseProjectile {

	public float forwardSpeed;
	public float spinSpeed;
	Vector3 movementDirection;

	protected override void Start(){

		base.Start();
		GetComponent<Rigidbody>().AddForce (transform.forward * forwardSpeed);

		movementDirection = Vector3.Normalize(target.position - transform.position);

		transform.Rotate(0, 0, 90);
	}

	protected override void Update(){

		base.Update ();

		if (isActive){
			transform.position += movementDirection * forwardSpeed * Time.deltaTime;
			transform.Rotate(0, 0, spinSpeed);
		}
	}
}