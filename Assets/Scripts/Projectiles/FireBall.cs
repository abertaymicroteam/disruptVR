using UnityEngine;
using System.Collections;

public class FireBall : BaseProjectile {

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
			transform.Rotate(spinSpeed * Random.Range(0.0f, 1.0f), spinSpeed * Random.Range(0.0f, 1.0f), spinSpeed * Random.Range(0.0f, 1.0f));
		}
	}
}