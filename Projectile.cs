using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float pLife = 10f;
	public Transform SpawnPoint;

	// Update is called once per frame
	void Update () 
	{
		pLife -= Time.deltaTime;
		if(pLife <= 0)
			Destroy(gameObject);
	}
	void OnCollisionEnter(Collision c)
	{
		Destroy(gameObject);
		if(c.gameObject.GetComponent("Player") != null)
		{
			Destroy(gameObject);
			c.gameObject.transform.position = SpawnPoint.position;
		}
	}
	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.GetComponent ("Sheild") != null)
			Destroy(gameObject);
	}
}
