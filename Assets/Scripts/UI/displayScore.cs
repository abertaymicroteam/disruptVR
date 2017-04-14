using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayScore : MonoBehaviour {

	public Text scoreText;
	public Text scoreText2;
	public Text scoreText3;

	private PlayerHealth health;
	public int score = 0;

	//private int score;
	// Use this for initialization
	void Start () 
	{
		health = GameObject.Find ("Player").GetComponent<PlayerHealth> ();
		score = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!health.dead) {
			scoreText.text = ("Score: " + score);
			scoreText2.text = ("Score: " + score);
			scoreText3.text = ("Score: " + score);
		} else {
			scoreText.text = ("Game Over");
			scoreText2.text = ("Game Over");
			scoreText3.text = ("Game Over");
		}
	}

	public void ballHit(){
		score += 1;
	}
}