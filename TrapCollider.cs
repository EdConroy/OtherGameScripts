using UnityEngine;
using System.Collections;

public class TrapCollider : MonoBehaviour {
	public Transform character, SpawnPoint, NextDoor;
	public int TrapType;
	enum Traps {LT_PIT, LT_DOOR};
	public void Activate()
	{
		Debug.Log("Active");
	}
	void Start () 
	{
	
	}
	void Update () 
	{

	}
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.GetComponent("Client") != null) //You can change this script to any script that only the player has
		{
			Debug.Log("Colliding");
			if (TrapType == (int) Traps.LT_PIT)
			{
				Pit p = new Pit();
				p.Activate(character,SpawnPoint);
			}
			else if(TrapType == (int) Traps.LT_DOOR)
			{
				Door d = new Door();
				d.Activate(character,NextDoor);
			}
		}
	}
}
