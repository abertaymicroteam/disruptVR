using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupLights : MonoBehaviour {

	void Start () {

		Transform[] transforms = GetComponentsInChildren<Transform> ();
		for (int i = 1; i < 9; i++){
			transforms [i].LookAt (Vector3.zero + new Vector3(0, GameObject.Find("Spawners").transform.position.y, 0));
		}
	}
}