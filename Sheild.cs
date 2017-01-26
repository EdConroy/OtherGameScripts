using UnityEngine;
using System.Collections;

public class Sheild : MonoBehaviour {
	public float shield_life = 10f;

	void Update () 
	{
		if(shield_life <= 0)
			Destroy(gameObject);
		shield_life -= Time.deltaTime;
	}
}
