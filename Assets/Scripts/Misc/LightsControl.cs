using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsControl : MonoBehaviour {

	Transform[] transforms;

	void Start () {

		transforms = GetComponentsInChildren<Transform> ();
		for (int i = 1; i < 9; i++){
			transforms [i].LookAt (Vector3.zero + new Vector3(0, GameObject.Find("Spawners").transform.position.y, 0));
		}
	}

	public void SetColor(Color color){

		Light[] lights = GetComponentsInChildren<Light> ();
		foreach (Light light in lights) {
			light.color = color;
		}
	}
}