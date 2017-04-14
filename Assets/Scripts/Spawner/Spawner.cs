﻿using UnityEngine;
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

	}

	public void Spawn(GameObject item){

		Instantiate (item, transform.position + new Vector3(0.0f, 0.9f, 0.0f), Quaternion.identity);
	}
}