
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VRPowerupManager : MonoBehaviour {

	public List<GameObject> powerup;

	public void Activate(int num) {
		Instantiate (powerup[num]);

		// increment hit counter for round stats
		ScoreTracker tracker = GameObject.Find("ScoreTracker").GetComponent<ScoreTracker>();
		tracker.AddPowerup();
	}
}