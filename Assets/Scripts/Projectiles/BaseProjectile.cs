using UnityEngine;
using System.Collections;
	
public class BaseProjectile : MonoBehaviour {

	protected Transform target;
	protected float spawnDistance;
	public bool isActive;
	public bool showIndicator;
	//public Vector3 movementDirection;

	protected virtual void Start(){
	
		//movementDirection = Vector3.Normalize(target.position - transform.position);

		target = GameObject.FindWithTag("Player").transform;
		spawnDistance = Vector3.Distance (transform.position, target.position);

		transform.LookAt (target);
	}

	protected virtual void Update(){

		if (Vector3.Distance (transform.position, target.position) >= (spawnDistance * 1.5f)) {
			Destroy (gameObject);

			// increment hit counter for round stats
			ScoreTracker tracker = GameObject.Find("ScoreTracker").GetComponent<ScoreTracker>();
			tracker.AddDodge();
		}
	}

	public void setVelocity(Vector3 vel){

		GetComponent<Rigidbody> ().velocity = vel;
	}
}