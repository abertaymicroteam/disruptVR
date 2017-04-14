using UnityEngine;
using System.Collections;

public class Shield : BasePowerup {

	// Collision counter
	public int durability;
	private int collisions;

	// Use this for initialization
	protected override void Start () 
	{
		collisions = 0;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		if (collisions == durability) 
		{
			// Destroy shield after two collisions
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		collisions++;
		if (collision.gameObject.tag == "Projectile") {
			
			Vector3 newDir = collision.collider.transform.position - transform.position;

			collision.gameObject.GetComponent<Rigidbody>().AddForce(newDir, ForceMode.VelocityChange);

		}

		// increment hit counter for round stats
		ScoreTracker tracker = GameObject.Find("ScoreTracker").GetComponent<ScoreTracker>();
		tracker.AddShield();
	}
}