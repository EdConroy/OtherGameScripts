using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {

	public Transform SpawnPoint;

	void OnCollisionEnter(Collision c)
	{
		if(c.gameObject.GetComponent("Player") != null)
		{
			c.gameObject.transform.position = SpawnPoint.position;
		}
	}
}
