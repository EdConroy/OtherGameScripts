using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
    
	// Use this for initialization
    Vector3 direction;
	void Awake () {
        rigidbody.freezeRotation = false;
        rigidbody.useGravity = false;
        direction = new Vector3((Random.value - .5f) * 5f, (Random.value - .5f) * 5f, (Random.value - .5f) * 5f);
        rigidbody.AddForce(direction,ForceMode.VelocityChange);
        //Debug.Log("Called");
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > 400 || transform.position.x < -400 || transform.position.y > 400 || transform.position.y < -400 || transform.position.z > 400 || transform.position.z < -400)
        {
            Destroy(this.gameObject);
        }

	}
    
    void OnCollisionEnter(Collision c)
    {   Debug.Log("found");
   
        RaycastHit hit;
         Vector3 normal = direction;
        if (Physics.Raycast(transform.position,direction, out hit,1f))
        {   
          normal=hit.normal;
        }
        float mag = direction.magnitude;
        Debug.Log("direction  " + direction);
        direction = Vector3.Reflect(direction, normal);
        Debug.Log("Reflection  " + direction);
        direction.Normalize();
        direction =direction* mag;
        Debug.Log("After normalization  " + direction);
        rigidbody.AddForce(direction, ForceMode.VelocityChange);

    }
}
