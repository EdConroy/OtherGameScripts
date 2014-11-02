using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	GameObject[] rocks; // array of destructable traps
	public Transform spawnpoint, character; //teleportation point and reference to the character 
	void Start () 
	{
	
	}
	void Update () 
	{

	}
	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Colliding");
		if (other.CompareTag("trans"))
		{
			character.position = spawnpoint.position; //spawns the character at a location
		}
		else if(other.CompareTag("rock"))
		{
			DestroyRocks();
		}
	}
	void DestroyRocks()
	{
		rocks = GameObject.FindGameObjectsWithTag("rock");
		for(int i = 0; i < rocks.Length; i++) //You can change this to any value to simulate distance
			if(rocks[i])
				Destroy(rocks[i]);
	}

}
