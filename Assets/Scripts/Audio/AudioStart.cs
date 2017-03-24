using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStart : MonoBehaviour {

	public float beginAt;
	private float startTime;
	private bool playing;
	AudioSource sound;
	// Use this for initialization
	void Start () 
	{
		sound = this.gameObject.GetComponent<AudioSource>();
		startTime = 0;
		playing = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (((Time.time - startTime) > beginAt) && (playing == false)) 
		{
			sound.Play();
			playing = true;
		}
	}
}
