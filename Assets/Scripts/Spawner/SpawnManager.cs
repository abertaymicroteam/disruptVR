using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {

	public List<Spawner> spawners;
	public List<bool> enabledProjectiles;
	public float rotationSpeed;

	private GameManager manager;
	private float timer;

	void Start(){

		Random.InitState((int)System.DateTime.Now.Ticks);

		foreach (Spawner obj in GetComponentsInChildren<Spawner>()){
			spawners.Add (obj);
		}
		foreach (GameObject obj in spawners[0].Projectiles){
			enabledProjectiles.Add (true);
		}
		manager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	void Update(){

		if (manager.currentStage == GameManager.stage.ROUND) {
			timer += Time.deltaTime;
			if (timer >= manager.fireRate){
				spawners [Random.Range (0, spawners.Count)].Spawn ();
				timer = 0;
			}
		} else {
			timer = 0; 
		}

		// Rotate turrets around Y axis
		foreach(Spawner obj in spawners){
			transform.RotateAround (transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
		}
	}
}