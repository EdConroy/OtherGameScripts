using UnityEngine;
using System.Collections;

public class GravityField : MonoBehaviour
{
	void Start ()
	{
		//Do gravity fields need to act at a range? the answer will probably end up being yes.
		//if so, then in start, the game needs to identify which objects begin within a gravity field
	}

	void Update ()
	{
	
	}

//	void OnTriggerEnter(Collider c)
//	{
//		GameObject obj = c.gameObject;
//	}
//
//	void OnTriggerExit(Collider c)
//	{
//		GameObject obj = c.gameObject;
//
//		if(obj.tag == "p") //player
//		{
//			Type t = this.gameObject.collider.GetType(); //collider type
//
////			if      (t == TypeOf(   BoxCollider)) 
////			else if (t == TypeOf(SphereCollider)) 
//		}
//		else if(obj.tag == "c") //crate
//		{
//
//		}
//	}
}
