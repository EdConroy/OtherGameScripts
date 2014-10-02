using UnityEngine;
using System.Collections;

public class TrapCollider : MonoBehaviour {
	public Transform character, SpawnPoint, NextDoor;
	// Use this for initialization
	void Start () 
	{
	
	}
	// Update is called once per frame
	void Update () 
	{

	}
	void OnTriggerEnter(Collider other) 
	{
		Debug.Log("Colliding");
		if (other.CompareTag("pit"))
		{
			character.position = SpawnPoint.position;
		}
		if(other.CompareTag("door"))
		{
			character.position = NextDoor.position;
		}
	}
}
