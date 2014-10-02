using UnityEngine;
using System.Collections;

public class EpicCam : MonoBehaviour
{
	Transform trans;
	Transform target;
	Transform orbit;
	Vector3 oldOrbit;

	int layer = (1 << 0);
	
	// Use this for initialization
	void Start ()
	{
		GameObject obj = this.gameObject;
		trans = obj.transform;
		
		obj = GameObject.Find("/Player");
		orbit = obj.transform;
		
		obj = GameObject.Find("/Player/CameraTarget");
		target = obj.transform;
		
		oldOrbit = orbit.position;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		trans.position += (orbit.position - oldOrbit);
		
		if(trans.rotation != target.rotation)
		{
			//theory! Hold the last few positions for a particular distance in a vector (say 5m)//
			//if a particular position cannot see the center of the object, then pick the next//
			//iterate until you can see the player

			float angle = Quaternion.Angle(trans.rotation,target.rotation);
			
			trans.rotation = Quaternion.Slerp(trans.rotation,target.rotation,Time.deltaTime*90f/angle);

			Vector3 direction = -(trans.rotation*Vector3.forward); 

			float distance = 3.5f;

//			RaycastHit hit;
//
//			if(Physics.Raycast(orbit.position, direction, out hit, 5f, layer)) distance = hit.distance - 0.2f;
//			else                                                               distance = 5f;
			
			trans.position = orbit.position + direction*distance;
		}
		
		oldOrbit = orbit.position;
	}
	
	public void Rotate(Quaternion q)
	{
		target.rotation = q;
		target.position = orbit.position - trans.rotation*Vector3.forward*3.5f;
	}
}
