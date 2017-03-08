using UnityEngine;
using System.Collections;

public class SpiralBall : BaseProjectile {

	public float forwardSpeed;
	public float circlingSpeed;
	public float circleRadius;

	private float initTime;
	private float travelTime;

	protected override void Start(){

		base.Start ();
		initTime = Time.timeSinceLevelLoad;
		travelTime = spawnDistance / forwardSpeed;

		GetComponent<Rigidbody> ().AddForce (transform.forward * forwardSpeed);
	}
	
	protected override void Update(){
		
		base.Update ();

		if(isActive){
			
			float lifetime = Time.timeSinceLevelLoad - initTime;

			transform.position += transform.forward * forwardSpeed * Time.deltaTime;
			transform.position += transform.right * Mathf.Cos (lifetime * circlingSpeed) * circleRadius * Time.deltaTime;
			transform.position += transform.up * Mathf.Sin (lifetime * circlingSpeed) * circleRadius * Time.deltaTime;

			if (lifetime < 1.0f) {
				circleRadius += lifetime * Time.deltaTime;
			}
		}
	}
}