using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStop : MonoBehaviour {

	public float playLength;
	private float startTime;
	AudioSource sound;
	// Use this for initialization
	void Start () 
	{
		sound = this.gameObject.GetComponent<AudioSource>();
		startTime = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ((Time.time - startTime) > playLength) 
		{
			sound.Stop();
		}
	}
}
