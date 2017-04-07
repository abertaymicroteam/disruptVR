using UnityEngine;
using System.Collections;

public class ExplodingBall : BaseProjectile {

	private Rigidbody body;
	private Color color;

	public float explosionDmg;
	private float gravity = 9.8f;

	protected override void Start(){

		base.Start ();
		body = GetComponent<Rigidbody>();
		color = GetComponent<Renderer>().material.color;

		Throw();
	}

	protected override void Update(){

		base.Update();

		if (isActive) {
			if (Vector3.Distance (transform.position, target.position) < (spawnDistance / 2.0f)) {
				GetComponent<Renderer> ().material.color = Color.Lerp (color, Color.red, 0.5f);
			}
		} else {
			GetComponent<Renderer> ().material.color = Color.white;
		}
	}



	void Throw (){

		Vector3 direction = Vector3.Normalize(target.position - transform.position);

		float theta = 45.0f;
		direction.y = (theta / 90.0f);

		float velocity = Mathf.Sqrt(gravity * spawnDistance / (Mathf.Sin(2 * theta * Mathf.Deg2Rad)));

		body.AddForce(direction * velocity * 50.0f);
	}
}