using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {
    public Transform origin, dest, rotationPoint;
    public float platformSpeed;
    private bool pivot = false;
    private Quaternion rotationOrigin;
    void Start()
    {
        rotationOrigin = transform.rotation;
    }
	void FixedUpdate() 
    {
        if(rotationPoint!=null)
        {
            transform.RotateAround(rotationPoint.position, Vector3.right, platformSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationOrigin, 1f);
        }
        else if(origin!=null&&dest!=null)
        {
            if(transform.position == dest.position)
                pivot = true;
            if(transform.position == origin.position)
                pivot = false;
            if(pivot)
                transform.position = Vector3.MoveTowards(transform.position, origin.position, platformSpeed);
            else
                transform.position = Vector3.MoveTowards(transform.position, dest.position, platformSpeed);
        }
	}
}
