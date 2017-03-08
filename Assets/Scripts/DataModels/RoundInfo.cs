using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundInfo {

	public int roundTime;
	public int breakTime;
	public int projectileCount;

	[HideInInspector]
	public int hits, dodges, punches, powerups, shielded, detonated, shockwaved;

	// These variables are incremented whenever they happen in scripts:
	// PlayerHealth.cs
	// BaseProjectile.cs
	// SphereCollisionScript.cs
	// PowerupManager.cs
	// Shield.cs
	// Detonate.cs
	// Shockwave.cs
}