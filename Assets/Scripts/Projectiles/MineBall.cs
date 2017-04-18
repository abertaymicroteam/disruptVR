using UnityEngine;
using System.Collections;

public class MineBall : BaseProjectile {

	public float forwardSpeed;
	public float spinSpeed;

	public float explosionDmg;
	Vector3 movementDirection;

	protected override void Start(){

		base.Start();
		GetComponent<Rigidbody>().AddForce (transform.forward * forwardSpeed);

		movementDirection = Vector3.Normalize(target.position - transform.position);
	}

	protected override void Update(){

		base.Update ();

		if (isActive){
			transform.position += movementDirection * forwardSpeed * Time.deltaTime;
			transform.Rotate(spinSpeed, 0, 0);
		}
	}

	void OnTriggerEnter(Collider other){

		if (other.CompareTag ("Player")){
			other.GetComponent<PlayerHealth>().playerHealth -= explosionDmg;
			Destroy (gameObject);
		}
	}
}