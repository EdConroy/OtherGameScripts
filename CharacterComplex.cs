using UnityEngine;
using System.Collections.Generic;


public class CharacterComplex : MonoBehaviour
{
	List<GameObject> bField = new List<GameObject>();
	List<GameObject> sField = new List<GameObject>();

	Rigidbody rigid;
	Transform trans;
	
	EpicCam cam;
		
	int layer = (1 << 0); //1 shifted zero units left, aka 1, the default layer and no others
						  //there are 32 zeros/ones for the 32 layers, each one is a boolean indicating
						  //whether collisions are possible between the layers

	float speed = 6f;
	float xSensitivity = 100f;
	
	Vector3 normal = Vector3.up;
	Vector3 direction = Vector3.zero;
	
	// Use this for initialization
	void Start ()
	{
		GameObject obj = GameObject.Find("/MainCamera");
		cam = (EpicCam) obj.GetComponent("EpicCam");

		rigid = this.rigidbody;
		trans = this.gameObject.transform;
	}

	void Update()
	{
		rigid.velocity = Vector3.zero;
	}

	void FixedUpdate ()
	{
		trans.Rotate(Vector3.up, Input.GetAxis("Mouse X")*xSensitivity*Time.deltaTime, Space.Self);

		Vector3 direction = MoveDir();
		
		RaycastHit hit;
		
		if(direction != Vector3.zero && Physics.SphereCast(trans.position,0.5f,-normal, out hit, 1f, layer))
		{
			Vector3 forward = trans.forward;
			if(normal != hit.normal) forward = PlanarMovement(normal,hit.normal,trans.forward);
			
			if(forward != Vector3.zero)
			{
				trans.rotation = Quaternion.LookRotation(forward,hit.normal); //rotate the player base
				cam.Rotate(Quaternion.LookRotation(forward,hit.normal)); //rotate the target position for the camera
			}
			
			normal = hit.normal;
			
			direction = MoveDir();
		}
		
		if(direction != Vector3.zero && Physics.SphereCast(trans.position,0.5f,direction.normalized, out hit, direction.magnitude, layer))
		{
			Vector3 forward = PlanarMovement(normal,hit.normal,trans.forward);
			
			if(forward != Vector3.zero)
			{
				trans.rotation = Quaternion.LookRotation(forward,hit.normal); //rotate the player base
				cam.Rotate(Quaternion.LookRotation(forward,hit.normal)); //rotate the target position for the camera
			}
			
			normal = hit.normal;
			
			direction = MoveDir();
		}
		
		trans.Translate(direction, Space.World);
	}
	
	Vector3 MoveDir()
	{
		Vector3 input = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")).normalized;
		
		Vector3 dir = trans.rotation*input; //move respective to player rotation
		
		return dir*speed*Time.deltaTime;
	}
	
	Vector3 PlanarMovement (Vector3 normal, Vector3 next_normal, Vector3 vect)
	{
		Vector3 intersection = Vector3.Cross(normal, next_normal).normalized; //the component along the intersection will be the same.
		
		Vector3 oldBinormal  = Vector3.Cross(intersection,      normal).normalized; //the directions normal to the intersection and "up"
		Vector3 newBinormal  = Vector3.Cross(intersection, next_normal).normalized;
		
		vect = Vector3.Project(vect, intersection) /*the old component*/ + newBinormal*Vector3.Dot(vect, oldBinormal); /*the new component*/
		
		return vect;
	}

//	Vector3 MovingPlatform()
//	{
//		//moving platforms will be based on parametric equations, where t is used (e.g. with a cosine) to determine the position of the platform//
//
//		//to move the player around a circle, the distance between the player and the orbit center must be calculated each frame
//		//the player's old relative position to the orbit center must be subtracted on the subsequent frame to be replaced by the
//		//position according to the new t value.
//
//		//for regular moving platforms, the delta position of the platform can be sent instead, since player height does not affect the equation.
//	}

//	void OnTriggerEnter(Collider c) //add field for consideration
//	{
//		Type type = c.GetType();
//		
//		if (t == TypeOf(BoxCollider)) 
//		{
//			bField.Add(c.gameObject);
//		}
//		else if (t == TypeOf(SphereCollider))
//		{
//			sField.Add(c.gameObject);
//		}
//	}

//	void OnTriggerExit(Collider c) //field can no longer affect the player
//	{
//		flux.Remove(c.gameObject);
//
//		//turn off gravity field's effect
//	}
}
