using UnityEngine;
using System.Collections;

public class GrapplePoint : MonoBehaviour {

	public float grap_cool = 1f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		grap_cool -= Time.deltaTime;
		if(grap_cool <= 0)
		{
			Destroy(gameObject);
			grap_cool = 0f;
		}	
	}
}
