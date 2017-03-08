using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupManager : MonoBehaviour {

	public List<GameObject> powerup;

	public void Activate(int num) {
		Instantiate (powerup[num]);

		// increment hit counter for round stats
		GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
		manager.rounds [manager.round_].powerups++;
	}
}