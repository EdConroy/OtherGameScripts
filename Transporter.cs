using UnityEngine;
using System.Collections;

public class Transporter : PowerUp
{
	public void Activate(Transform character, Transform spawnpoint)
	{
		character.position = spawnpoint.position; //spawns the character at a location
	}
}
