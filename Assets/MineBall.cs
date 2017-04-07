using UnityEngine;
using System.Collections;

public class MineBall : BaseProjectile {

	public float forwardSpeed;
	public float spinSpeed;

	protected override void Start(){

		base.Start();
		GetComponent<Rigidbody>().AddForce (transform.forward * forwardSpeed);
	}

	protected override void Update(){

		base.Update ();

		Vector3 movementDirection = Vector3.Normalize(target.position - transform.position);

		if (isActive){
			transform.position += movementDirection * forwardSpeed * Time.deltaTime;
			transform.Rotate(spinSpeed, 0, 0);
		}
	}
}