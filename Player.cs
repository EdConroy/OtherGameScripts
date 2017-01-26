using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	
	bool canJump;
	public int num_jumps;
	public float jump_height;
	public Rigidbody projectile;
	public Rigidbody shield;
	public Transform pSpawn;
	public float proj_cool = 0f;
	public float def_cool = 0f;

	void Start () 
	{
		canJump = false;
	}

	void Update () 
	{
		if(!Input.GetMouseButtonDown(0))
		{
			transform.position += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * 5f;
			transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * 5f;
		}
		if(Input.GetButtonDown("Jump") && (num_jumps > 0) && canJump)
		{
			--num_jumps;
			GetComponent<Rigidbody>().velocity = transform.up * jump_height;
			if(num_jumps <= 0)
				canJump = false;
		}
		if(Input.GetMouseButtonUp(1) && proj_cool == 0)
		{
			Rigidbody shot = Instantiate(projectile, pSpawn.position, pSpawn.rotation) as Rigidbody;
			shot.AddForce(pSpawn.forward * 1000f);
			proj_cool = 3f;
		}
		if(proj_cool > 0f)
			proj_cool -= Time.deltaTime;
		else
			proj_cool = 0f;
		if(Input.GetKeyDown(KeyCode.G) && def_cool == 0)
		{
			Rigidbody def = Instantiate(shield, transform.position, transform.rotation) as Rigidbody;
			def_cool = 15f;
		}
		if(Input.GetMouseButtonDown(0))
			canJump = false;
		else canJump = true;
		if(def_cool > 0f)
			def_cool -= Time.deltaTime;
		else
			def_cool = 0f;

	}
	void OnCollisionEnter()
	{
		canJump = true;
		num_jumps = 2;
	}
}
