using UnityEngine;
using System.Collections;

public class ThrowBall : BaseProjectile {

	private Rigidbody body;

	private float gravity = 9.8f;

	protected override void Start(){

		base.Start ();
		body = GetComponent<Rigidbody>();

		Throw();
	}
		
	protected override void Update(){

		base.Update ();
	}

	void Throw(){

		Vector3 direction = (target.position - transform.position).normalized;
		float distance = Vector3.Distance(transform.position, target.transform.position);

		float theta = 45.0f;
		direction.y = (theta / 90.0f);

		float velocity = Mathf.Sqrt(gravity * distance / (Mathf.Sin(2 * theta * Mathf.Deg2Rad)));

		body.AddForce(direction * velocity * 50.0f);
	}
}