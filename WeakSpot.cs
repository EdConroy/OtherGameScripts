using UnityEngine;
using System.Collections;

public class WeakSpot : MonoBehaviour {

	public GameObject self;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.GetComponent("Player") != null)
			Destroy(self);
	}
}
