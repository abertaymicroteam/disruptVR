using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Detonate : BasePowerup {

	public GameObject explosionEffect;
	BaseProjectile[] projectiles;

	protected override void Start () 
	{
		base.Start ();

		projectiles = GameObject.FindObjectsOfType<BaseProjectile> ();
		foreach (BaseProjectile ball in projectiles) 
		{
			Instantiate (explosionEffect, ball.transform.position, ball.transform.rotation);
			Destroy (ball.gameObject);

			// increment hit counter for round stats
			GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
			manager.rounds [manager.round_].detonated++;
		}
	}

	protected override void Update() 
	{
		base.Update ();
	}
}