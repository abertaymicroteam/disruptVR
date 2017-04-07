using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shockwave : BasePowerup {

	protected override void Start () {
		base.Start ();

	}

	protected override void Update() {
		base.Update ();

		BaseProjectile[] projectiles = GameObject.FindObjectsOfType<BaseProjectile> ();
		foreach (BaseProjectile ball in projectiles) {
			if (Vector3.Distance (Vector3.zero, ball.transform.position) < 3.0f) {
				ball.isActive = false;

				// increment hit counter for round stats
				ScoreTracker tracker = GameObject.Find("ScoreTracker").GetComponent<ScoreTracker>();
				tracker.AddShockwave();
			}
			ball.GetComponent<Rigidbody> ().AddForce (ball.transform.position, ForceMode.Acceleration);
		}
	}

	void OnTriggerStay(Collider other){

		if (other.GetComponent<BaseProjectile> ()) {
			other.GetComponent<BaseProjectile> ().isActive = false;
			other.GetComponent<Rigidbody> ().useGravity = false;
			other.GetComponent<Rigidbody> ().AddForce (other.transform.position * 10, ForceMode.Acceleration);
		}
	}
}