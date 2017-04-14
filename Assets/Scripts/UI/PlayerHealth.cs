using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	//players health
	public float playerHealth;
	[Tooltip("How much health to regen per second")]
	public float regenRate;
	[Tooltip("How long since damage to begin regenerating")]
	public float regenStart;
	private float regenInterval;
	float regenTimer;

	// Object containers

	SpriteRenderer damage;
	//AnnouncerScript aScript;

	// Hurt counter and bool
	bool hurtAudioFlag;
	public bool dead;
	float deadtime;

	// Use this for initialization
	void Start ()
	{
		playerHealth = 100.0f;
		regenRate = 10.0f;
		regenStart = 0.5f;
		regenInterval = 0.1f;

		damage = GameObject.FindGameObjectWithTag ("Damage").GetComponent<SpriteRenderer> ();

		//aScript = GameObject.Find ("Announcer").GetComponent<AnnouncerScript> ();

		// Audio parameters
		dead = false;
		hurtAudioFlag = false;
		regenTimer = 0;

		// Start health regen routine
		StartCoroutine(RegenHealth());
	}
	
	// Update is called once per frame
	void Update () 
	{
		

		if (playerHealth <= 0.0f) 
		{
			//dead = true;
			//GameOver();
		}
		if (dead)
		{
			deadtime += Time.deltaTime;
			if (deadtime >= 5.0f){
				dead = true;
				deadtime = 0;
			}
		}

		else if (playerHealth > 100)
		{
			playerHealth = 100;
		}

		if ((regenTimer > 10) && (hurtAudioFlag == false))
		{
			Debug.Log ("Hurt");
			//aScript.snores = true;
			hurtAudioFlag = true;
		}

		// Update UI Damage
		float damAlpha = Mathf.Abs((playerHealth / 100) - 1);
		damage.color = new Vector4(damage.color.r, damage.color.g, damage.color.b, damAlpha);

		// increase timer
		regenTimer += Time.deltaTime;
	}

	//Check for collision between player and a projectile
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Projectile") 
		{
			playerHealth -= 34.0f;

			// Reset hurt timer
			regenTimer = 0;
		}
	}

	private IEnumerator RegenHealth()
	{
		while (playerHealth > 0) 
		{
			if (regenTimer > regenStart && playerHealth < 100) 
			{
				playerHealth += regenRate * regenInterval;
			}
			yield return new WaitForSeconds (regenInterval);
		}
	}

	void GameOver(){

		// Turn spawners off
		SpawnManager spawnman = GameObject.Find ("Spawners").GetComponent<SpawnManager> ();
		spawnman.TurnOff ();
		GameManager gameman = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		gameman.reset ();
		gameman.round_ = 0;
		playerHealth = 100.0f;
		dead = false;
	}
}