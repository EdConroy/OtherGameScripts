using UnityEngine;
using System.Collections;

public class Tools : MonoBehaviour {
    public static bool isGrappled = false;
    public Transform point;
    private RaycastHit gPoint, target;
    private Vector3 dist;
    private Quaternion origin, camera_position;
	public GameObject grap_point;

	void Start () 
    {
        origin = transform.rotation;
        camera_position = Camera.main.GetComponent<Camera>().transform.rotation;
	}
	void Update () 
    {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//casts a ray based on the cursor's location
		if(!isGrappled && Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out gPoint, 50))//Grapple Script
		{
			if(!gPoint.transform.gameObject.CompareTag("Border") && 
			   !gPoint.transform.gameObject.GetComponent("Enemy"))
			{
				isGrappled = true;
				dist = gPoint.point;
				point = gPoint.collider.gameObject.transform;
				Instantiate(grap_point, dist, point.rotation);
				Debug.DrawRay(dist,-ray.direction* gPoint.distance,Color.green,10f);
				transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
		}
		/*
	     * The below code allows the player to reel in the grappling hook and extend the grappling hook
	     */
        if(isGrappled && !Input.GetMouseButtonUp(0))
        {
			gameObject.GetComponent<Rigidbody>().useGravity = false;
			if(Input.GetKey("right"))
				transform.RotateAround(point.position, Vector3.right, 10 * Time.deltaTime);
			if(Input.GetKey("left"))
                transform.RotateAround(point.position, -Vector3.right, 10 * Time.deltaTime);
			if(Input.GetKey("up"))
				transform.position = Vector3.MoveTowards(transform.position, dist, 10 * Time.deltaTime);
			if(Input.GetKey("down"))
				transform.position += point.forward * Input.GetAxis("Vertical") * Time.deltaTime * 10f;
        }
        if(isGrappled && Input.GetMouseButtonUp(0))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, origin, 1f);//resets the player position once left mouse button is released
            Camera.main.GetComponent<Camera>().transform.rotation = camera_position; //Realign the camera
            isGrappled = false;
			gameObject.GetComponent<Rigidbody>().useGravity = true; 
        }
	}
	void OnCollisionEnter()
	{
		if(isGrappled)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, origin, 1f);//resets the player position once left mouse button is released
			Camera.main.GetComponent<Camera>().transform.rotation = camera_position;
			isGrappled = false;
			gameObject.GetComponent<Rigidbody>().useGravity = true;
		}
	}
}
