<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundInfo {

	public int roundTime;
	public int breakTime;
	[Range(0, 10)]
	public List<int> projectiles;

	public int totalProjectiles(){

		int sum = 0;
		foreach (int item in projectiles) {
			sum += item;
		}
		return sum;
	}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundInfo {

	public int roundTime;
	public int breakTime;
	[Range(0, 30)]
	public List<int> projectiles;

	public int totalProjectiles(){

		int sum = 0;
		foreach (int item in projectiles) {
			sum += item;
		}
		return sum;
	}
>>>>>>> 76674f799a1ff3383a4b06963af62541df05bc93
}