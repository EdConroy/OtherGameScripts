using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
	public Transform spawnpoint, character;
	public int PowerUpType;
	enum PowerUps {LT_TRANS, LT_EXPL};
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
			if (PowerUpType == (int) PowerUps.LT_TRANS)
			{
				Transporter trans = new Transporter();
				trans.Activate(character, spawnpoint);
			}
			else if(PowerUpType == (int) PowerUps.LT_EXPL)
			{
				Dynamite exp = new Dynamite();
				exp.Activate();
			}
		}
	}
}
