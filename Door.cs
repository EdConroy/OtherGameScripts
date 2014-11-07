using UnityEngine;
using System.Collections;

public class Door : TrapCollider
{
	public void Activate(Transform character, Transform NextDoor)
	{
		character.position = NextDoor.position;
	}
}
