using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {

	public bool isactive;
	private List<Spawner> spawners;
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

	void Update (){

		if (isactive) {

			timer += Time.deltaTime;
			if (timer >= fireRate) {

				timer = 0;
				Fire ();
			}

			// Rotate turrets around Y axis
			transform.RotateAround (transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
		}
	}

	void Fire (){

		// Choose a random projectile from the list
		// ballnum is 0, 1, 2 or 3

		if (info.totalProjectiles() == 0) {
			//nothing to fire 
			return;
		}

		int ballnum = 0;
		do {
			ballnum = Random.Range (0, info.projectiles.Count);
		} while (info.projectiles[ballnum] == 0);

		// Remove an item from the list
		info.projectiles[ballnum]--;

		// Spawn the projectile we chose from a random spawner
		GameObject ball = GameObject.Find("GameManager").GetComponent<GameManager>().allProjectiles[ballnum];
		spawners [Random.Range (0, spawners.Count)].Spawn(ball);
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
}