using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public List<RoundInfo> rounds;
	[HideInInspector]
	public int round_;

	private float timer;
	[HideInInspector]
	public float fireRate;
	private SpawnManager spawnman;
	private PowerupManager powerman;
	public enum stage {ENTRANCE, ROUND, BREAK};
	public stage currentStage;

	private GameObject rig;

	void Start(){

		round_ = 0;
		timer = 0;

		currentStage = stage.ENTRANCE;
		spawnman = GameObject.Find("Spawners").GetComponent<SpawnManager>();
		powerman = GetComponent<PowerupManager> ();

		rig = GameObject.Find("[CameraRig]");
		CalculateFireRate ();
	}

	void Update(){

		switch (currentStage){
		case stage.ENTRANCE:

			if (rig.transform.position.y == 0.0f){
				currentStage = stage.ROUND;
			}
			break;
		case stage.ROUND:

			timer += Time.deltaTime;
			if (timer >= (float)rounds[round_].roundTime){
				currentStage = stage.BREAK;
				EndRound ();
			}
			break;
		case stage.BREAK:

			timer += Time.deltaTime;
			if (timer >= (float)rounds[round_].breakTime){
				currentStage = stage.ROUND;
				EndBreak ();
			}
			break;
		}
	}

	void EndRound(){

		timer = 0;

		// Spawn a detonate to destroy all balls
		powerman.Activate(0);
	}

	void EndBreak(){

		timer = 0;

		if (round_ < rounds.Count) {
			round_++;
		}

		CalculateFireRate ();
	}

	void CalculateFireRate(){
		
		fireRate = (float)rounds [round_].roundTime / (float)rounds [round_].projectileCount;
	}
}