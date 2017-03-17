using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public List<RoundInfo> rounds;
	//[HideInInspector]
	public int round_;

	private float timer;
	private GameObject rig;
	private SpawnManager spawnman;
	public enum stage {Entrance, Round, Wait, Break, GetReady};
	public enum loopMode {LoopFromStart, ReplayLast};
	public stage currentStage;
	public loopMode loop;

	void Start(){

		round_ = 0;
		timer = 0;
		currentStage = stage.Entrance;

		rig = GameObject.Find("[CameraRig]");
		spawnman = GameObject.Find("Spawners").GetComponent<SpawnManager>();
		spawnman.SetRoundInfo(rounds[0]);

//		foreach (RoundInfo round in rounds){
//			foreach (GameObject item in spawnman.allProjectiles){
//				round.projectiles.Add (4);
//			}
//		}
	}

	void Update(){

		RoundInfo thisRound = rounds[round_];

		switch (currentStage){
		case stage.Entrance:

			if (rig.transform.position.y >= -1.0f){
				currentStage = stage.Round;
				spawnman.TurnOn();
			}
			break;
		case stage.Round:

			if (timer == 0.0f){
				StartRound(thisRound);
			}
			
			timer += Time.deltaTime;

			if (timer >= (float)thisRound.roundTime){
				timer = 0;
				EndRound();
			}
			break;
		case stage.Wait:

			timer += Time.deltaTime;

			if (timer >= 5.0f){
				timer = 0;
				EndWait();
			}
			break;
		case stage.Break:

			if (timer == 0.0f){
				StartBreak();
			}

			timer += Time.deltaTime;

			if (timer >= (float)thisRound.breakTime){
				timer = 0;
				EndBreak();
			}
			break;
		case stage.GetReady:

			timer += Time.deltaTime;
			if (timer >= 3.0f) {
				timer = 0;
				currentStage = stage.Round;
			}
			break;
		}
	}

	void StartRound(RoundInfo round){

		spawnman.SetRoundInfo(round);
	}

	void EndRound(){

		currentStage = stage.Wait;
		spawnman.TurnOff();
	}

	void EndWait(){

		currentStage = stage.Break;

		BaseProjectile[] projectiles = GameObject.FindObjectsOfType<BaseProjectile>();
		foreach (BaseProjectile ball in projectiles){

			Destroy (ball.gameObject);
		}
	}

	void StartBreak(){

		// Raise the screen and ring objects so the player can see them
		// Display the score for that round
	}

	void EndBreak(){

		currentStage = stage.GetReady;

		// If we just finished the last round, decide what to do based on LoopMode
		if ((round_ + 1) == rounds.Count){

			switch (loop){
			case loopMode.LoopFromStart:
				round_ = 0;
				foreach (RoundInfo round in rounds){
					for (int i = 0; i < round.projectiles.Count; i++){
						round.projectiles [i]++;
					}
					round.roundTime += 2;
				}
				break;
			case loopMode.ReplayLast:
				
				break;
			}
		} else {
			round_++;
		}

		// Update spawn manager with the correct round
		spawnman.SetRoundInfo(rounds[round_]);
		spawnman.TurnOn();
	}
		
}