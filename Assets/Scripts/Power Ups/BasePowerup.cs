using UnityEngine;
using System.Collections;

public class BasePowerup : MonoBehaviour {

	private float aliveTime;
	private float timer;

	protected virtual void Start()
	{
		aliveTime = 2.0f;
		timer = 0.0f;
	}

	protected virtual void Update()
	{
		timer += Time.deltaTime;
		if (timer >= aliveTime) 
		{
			Destroy (gameObject);
		}
	}
}