﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayScore : MonoBehaviour {

	public Text scoreText;
	public Text scoreText2;
	public Text scoreText3;
	public Text scoreText4;

	private int score;
	private ScoreTracker tracker;

	// Use this for initialization
	void Start () 
	{
		tracker = GameObject.Find("ScoreTracker").GetComponent<ScoreTracker>();
		score = tracker.scores [0].hits;
	}
	
	// Update is called once per frame
	void Update () 
	{
		scoreText.text = ("Score: " + score);
		scoreText2.text = ("Score: " + score);
		scoreText3.text = ("Score: " + score);
		scoreText4.text = ("Score: " + score);
	}
}