using UnityEngine;
using System.Collections;

public class Dynamite : PowerUp
{
	GameObject[] rocks; // array of destructable traps
	public void Activate()
	{
		rocks = GameObject.FindGameObjectsWithTag("rock");
		for(int i = 0; i < rocks.Length; i++) //You can change this to any value to simulate distance
			if(rocks[i])
				Destroy(rocks[i]);
	}
}
