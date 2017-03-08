using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public List<GameObject> Projectiles;
	private Transform target;

	private SpawnManager manager;

	void Start(){

		target = GameObject.FindWithTag("Player").transform;
		manager = GameObject.FindWithTag ("SpawnManager").GetComponent<SpawnManager> ();

		Random.InitState((int)System.DateTime.Now.Ticks);
	}

	void Update(){

		Vector3 direction = target.position - transform.position;
		transform.LookAt (target.position);
		transform.Rotate (transform.up, 90);
	}

	public void Spawn(){

		int next = Random.Range (0, Projectiles.Count);

		if (manager.enabledProjectiles [next]) {
			Instantiate (Projectiles [next], transform.position, Quaternion.identity);
		}
	}
}