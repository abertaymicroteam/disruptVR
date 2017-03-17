using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

	public float scale;

	void Update () {

		transform.Rotate(Vector3.up, scale * Time.deltaTime);
	}
}
