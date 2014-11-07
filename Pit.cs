using UnityEngine;
using System.Collections;

public class Pit : TrapCollider
{
	public void Activate(Transform character, Transform SpawnPoint)
	{
		character.position = SpawnPoint.position;
	}
}
