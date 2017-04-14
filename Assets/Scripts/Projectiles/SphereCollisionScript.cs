
using UnityEngine;
using System.Collections;

/*Sphere collision script
 * 
 * If the object leaves the arena or collides with the player delete the object
 * 
 * 
 */

public class SphereCollisionScript : MonoBehaviour {


	public displayScore score_;

	// Use this for initialization
	void Start () 
	{
		score_ = FindObjectOfType<displayScore> ();
	}

	// Update is called once per frame
	void Update () 
	{

	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "GameController") 
		{
			// vibrate controller and play hit audio
			SteamVR_Controller.Input ((int)collision.gameObject.GetComponentInParent<SteamVR_TrackedObject>().index).TriggerHapticPulse(3999);
			GetComponent<AudioSource> ().Play ();

			Debug.Log ("Adding Velocity!");
			ControllerVelocity controller = collision.gameObject.GetComponent<ControllerVelocity> ();

			Vector3 newDir = collision.collider.transform.position - transform.position;
			newDir.y = controller.GetVelocity ().y;
			newDir.x = controller.GetVelocity ().x;
			newDir.z = controller.GetVelocity ().z;

			GetComponent<Rigidbody>().AddForce(newDir, ForceMode.VelocityChange);

			// increment hit counter for round stats
			ScoreTracker tracker = GameObject.Find("ScoreTracker").GetComponent<ScoreTracker>();
			tracker.AddPunch();

			score_.ballHit ();

		}
	}

	// Destroy on collision with player
	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player") 
		{
			Destroy (gameObject);
		}
	}

	//Destroy if leaving the arena
	void OnTriggerExit(Collider collider){

		if (collider.tag == "Arena") {
			Destroy (gameObject);
		}
	}
}