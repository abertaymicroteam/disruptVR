using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour {

	//[HideInInspector]
	public List<RoundScore> scores;
	private GameManager manager;

	private float timer;

	void Start(){

		manager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		foreach (RoundInfo round in manager.rounds) {
			scores.Add(new RoundScore());
		}

		timer = 0;
	}

	void Update(){

		timer += Time.deltaTime;
		if (timer >= 2.0f) {
			timer = 0;
			Debug.Log (scores [0].hits);
		}

	}

	public void AddHit(){scores [manager.round_].hits++;}
	public void AddDodge(){scores [manager.round_].dodges++;}
	public void AddPunch(){scores [manager.round_].punches++;}
	public void AddPowerup(){scores [manager.round_].powerups++;}
	public void AddShield(){scores [manager.round_].shielded++;}
	public void AddDetonate(){scores [manager.round_].detonated++;}
	public void AddShockwave(){scores [manager.round_].shockwaved++;}

	// These variables are incremented whenever they happen in scripts:
	// PlayerHealth.cs
	// BaseProjectile.cs
	// SphereCollisionScript.cs
	// PowerupManager.cs
	// Shield.cs
	// Detonate.cs
	// Shockwave.cs
}