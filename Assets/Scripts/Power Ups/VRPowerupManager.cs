<<<<<<< HEAD:Assets/Scripts/Power Ups/VRPowerupManager.cs
﻿using UnityEngine;
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
=======
﻿using UnityEngine;
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
>>>>>>> 76674f799a1ff3383a4b06963af62541df05bc93:Assets/Scripts/Power Ups/VRPowerupManager.cs
}