using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayScore : MonoBehaviour {

	public Text scoreText;
	public Text scoreText2;
	public Text scoreText3;

	public int score = 0;

	//private int score;
	// Use this for initialization
	void Start () 
	{
		score = ScoreTracker.scores[0].hits;
	}
	
	// Update is called once per frame
	void Update () 
	{
		scoreText.text = ("Score: " + score);
		scoreText2.text = ("Score: " + score);
		scoreText3.text = ("Score: " + score);
	}
}
