<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {

	public bool isactive;
	private List<Spawner> spawners;
	public List<GameObject> allProjectiles;
	public float rotationSpeed;

	public float timer;
	public float fireRate;

	public RoundInfo info;
	public void SetRoundInfo (RoundInfo i) {info = i;}

	void Awake(){

		Random.InitState((int)System.DateTime.Now.Ticks);
		isactive = false;
		spawners = new List<Spawner> ();
		GetSpawnerList ();
	}

	void Update(){

		if (isactive){
			timer += Time.deltaTime;
			if (timer >= fireRate){

				timer = 0;
				Fire();
			}
		}

		// Rotate turrets around Y axis
		transform.RotateAround (transform.position, Vector3.up, rotationSpeed * Time.deltaTime);

	}

	void Fire(){

		// Choose a random projectile from the list
		// ballnum is 0, 1, 2 or 3
		int ballnum = Random.Range (0, info.projectiles.Count);
		info.projectiles [ballnum]--;

		// Spawn the projectile we chose from a random spawner
		GameObject ball = allProjectiles[ballnum];
		spawners [Random.Range (0, spawners.Count)].Spawn(ball);

		// if there are no more of the ball we just fired, remove it from the list
		if (info.projectiles[ballnum] <= 0) {
			info.projectiles.RemoveAt(ballnum);
		}
			
		/////////////
		/// But if we remove an item at the beginning of the list,
		/// element 2 is now element 1 and so on...
		/// Need to get better way of preventing it from picking items that are 0
		/// /////////
	}

	public void TurnOn(){
		
		isactive = true;

		// Calculate fire rate
		fireRate = (float)info.roundTime / (float)info.totalProjectiles ();
	}

	public void TurnOff(){

		isactive = false;
	}

	void GetSpawnerList(){

		spawners.Clear();
		foreach (Spawner obj in GetComponentsInChildren<Spawner>()){
			spawners.Add (obj);
		}
	}
=======
﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {

	public bool isactive;
	private List<Spawner> spawners;
	public List<GameObject> allProjectiles;
	public float rotationSpeed;

	public float timer;
	public float fireRate;

	public RoundInfo info;
	public void SetRoundInfo (RoundInfo i) {info = i;}

	void Awake(){

		Random.InitState((int)System.DateTime.Now.Ticks);
		isactive = false;
		spawners = new List<Spawner> ();
		GetSpawnerList ();
	}

	void Update(){

		if (isactive){
			timer += Time.deltaTime;
			if (timer >= fireRate){

				timer = 0;
				Fire();
			}
		}

		// Rotate turrets around Y axis
		transform.RotateAround (transform.position, Vector3.up, rotationSpeed * Time.deltaTime);

	}

	void Fire(){

		// Choose a random projectile from the list
		// ballnum is 0, 1, 2 or 3
		int ballnum = Random.Range (0, info.projectiles.Count);
		info.projectiles [ballnum]--;

		// Spawn the projectile we chose from a random spawner
		GameObject ball = allProjectiles[ballnum];
		spawners [Random.Range (0, spawners.Count)].Spawn(ball);

		// if there are no more of the ball we just fired, remove it from the list
		if (info.projectiles[ballnum] <= 0) {
			info.projectiles.RemoveAt(ballnum);
		}
			
		/////////////
		/// But if we remove an item at the beginning of the list,
		/// element 2 is now element 1 and so on...
		/// Need to get better way of preventing it from picking items that are 0
		/// /////////
	}

	public void TurnOn(){
		
		isactive = true;

		// Calculate fire rate
		fireRate = (float)info.roundTime / (float)info.totalProjectiles ();
	}

	public void TurnOff(){

		isactive = false;
	}

	void GetSpawnerList(){

		spawners.Clear();
		foreach (Spawner obj in GetComponentsInChildren<Spawner>()){
			spawners.Add (obj);
		}
	}
>>>>>>> 76674f799a1ff3383a4b06963af62541df05bc93
}