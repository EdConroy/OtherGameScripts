using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	
	bool canJump;
	public int num_jumps;
	public float jump_height;

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
		if(Input.GetMouseButtonDown(0))
			canJump = false;
		else canJump = true;

	}
	void OnCollisionEnter()
	{
		canJump = true;
		num_jumps = 2;
	}
}
