using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematics : MonoBehaviour
{
    public AnimationClip clipName;
    Animation ani;
    public float distance;
    public GameObject player;
    public GameObject self;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        distance = Vector3.Distance(player.transform.position, self.transform.position);
        
        if(distance < 5.0f)
        {
            clipName.wrapMode = WrapMode.Loop;
            ani.Play(clipName.name);
            ani[clipName.name].speed = 0.1f;
        }	
	}
}
