using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour {
	public GameObject test;
	private Vector3 currentPos;
	void Start () 
	{
		currentPos = test.transform.position;
	}	
	void Update () 
	{
		Connect();
		Movement();
	}
	void Connect()
	{
		Network.Connect("127.0.0.1", 25000);
		Debug.Log("Connected");
	}
	[RPC]
	void Movement()
	{
		if(Input.GetKey("left"))
		{
			Debug.Log("left");
		}
		else if(Input.GetKey("right"))
		{
			Debug.Log("right");
		}
	}
	[RPC]
	void MoveObjectOnScreen()
	{
		if(Input.GetKey("left"))
			currentPos.x = (test.transform.position.x - 3f) * Time.deltaTime;
		else if(Input.GetKey("right"))
			currentPos.x = (test.transform.position.x + 3f) * Time.deltaTime;
		else if(Input.GetKey("up"))
			currentPos.y = (test.transform.position.y + 2f) * Time.deltaTime;
		test.transform.position = currentPos;
	}
}
