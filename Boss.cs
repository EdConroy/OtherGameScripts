using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	public Transform player, pSpawn, p_origin, p_dest;
	public int times_hit;
	public Vector3 origin;
	public Quaternion rotationalOrigin;
	public bool pivot = false, hunting = false;
	
	void Start () 
	{
		times_hit = 0;
		origin = transform.position;
		rotationalOrigin = transform.rotation;
	}

	void Update () 
	{
		if(p_origin != null && p_dest != null && !hunting)
		{
			if(transform.position == p_dest.position)
				pivot = true;
			if(transform.position == p_origin.position)
				pivot = false;
			if(pivot)
				transform.position = Vector3.MoveTowards(transform.position, p_origin.position, .05f);
			else
				transform.position = Vector3.MoveTowards(transform.position, p_dest.position, .05f);
		}
	}
	void OnTriggerStay(Collider c)
	{
		if(c.gameObject.GetComponent("Player") != null)
		{
			hunting = true;
			transform.LookAt(c.gameObject.transform);
			transform.position += transform.forward * 5f * Time.deltaTime;
			GetComponent<Rigidbody>().isKinematic = false;
		}
	}
	void OnCollisionEnter(Collision c)
	{
		if(c.gameObject.GetComponent("Player") != null)
		{
			c.gameObject.transform.position = pSpawn.position;
			transform.position = Vector3.MoveTowards(transform.position, origin, 100f);
			GetComponent<Rigidbody>().isKinematic = true;
			transform.rotation = Quaternion.Slerp(transform.rotation, rotationalOrigin, 1f);
			times_hit = 0;
			hunting = false;
		}
		if(c.gameObject.GetComponent("Projectile") != null)
		{
			++times_hit;
			if(times_hit == 3)
			{
				Destroy(gameObject);
				Application.Quit();
			}
		}
	}
}
