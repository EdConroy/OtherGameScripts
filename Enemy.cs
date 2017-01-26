using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float proj_cool = 0f;
	public Transform player;
	public Transform SpawnPoint;
	public Rigidbody projectile;
	public Transform pSpawn;
	public Transform e_origin, e_dest;
	public bool pivot = false;
	public bool is_proj = false;
	
	// Update is called once per frame
	void Update () 
	{
		if(e_origin != null && e_dest != null)
		{
			if(transform.position == e_dest.position)
				pivot = true;
			if(transform.position == e_origin.position)
				pivot = false;
			if(pivot)
				transform.position = Vector3.MoveTowards(transform.position, e_origin.position, .05f);
			else
				transform.position = Vector3.MoveTowards(transform.position, e_dest.position, .05f);
		}
		if(proj_cool > 0f)
			proj_cool -= Time.deltaTime;
		else
			proj_cool = 0f;
	}
	void OnTriggerStay(Collider c)
	{
		if(is_proj && c.gameObject.GetComponent("Player") != null && proj_cool == 0)
		{
			transform.LookAt(player);
			Rigidbody shot = Instantiate(projectile, pSpawn.position, pSpawn.rotation) as Rigidbody;
			shot.AddForce(pSpawn.forward * 100f);
			proj_cool = 4.5f;
		}
	}
	void OnCollisionStay(Collision c)
	{
		if(c.gameObject.GetComponent("Player") != null)
		{
			c.gameObject.transform.position = SpawnPoint.position;
		}
	}
}
