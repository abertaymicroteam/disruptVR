using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	private Transform target;
	private SpawnManager manager;

	void Awake(){

		Random.InitState((int)System.DateTime.Now.Ticks);

		target = GameObject.Find("Player").transform;
		manager = GameObject.Find("Spawners").GetComponent<SpawnManager> ();
	}

	void Update(){

		//Vector3 direction = target.position - transform.position;
		//transform.LookAt (target.position);
		//transform.Rotate (transform.up, 90);
	}

	public void Spawn(GameObject item){

		Instantiate (item, transform.position, Quaternion.identity);
	}
}