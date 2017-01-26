using UnityEngine;

using System.Collections;



public class CameraControllScript : MonoBehaviour 
	
{
	public Vector3 start_pos;
	public bool is_col = false;
	public Vector3 last_pos;
	void Start()
	{
		start_pos = transform.position;
	}
	void Update()
	{
		if(!is_col)
			last_pos = transform.position;
	}
	void OnTriggerStay(Collider c)
	{
		if(c.gameObject.tag == "Border")
		{
			is_col = true;
			transform.position = last_pos;
		}
	}
	void OnTriggerExit(Collider c)
	{
		if(c.gameObject.tag == "Border")
		{
			is_col = false;
		}
	}
}
