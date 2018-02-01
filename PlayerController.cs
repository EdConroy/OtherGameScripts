using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    bool startTapTimer;
    bool startSnailTimer;
    bool grabbing;
    public int snailsCaught;
    RaycastHit hit;
    float snailTimer;
    float tapTimer;
    float timeMoving;
    float timeStopped;
    public float grabRange;
    public float lookRange;
    float snailLookTime;
    float lobsterLookTime;
    float reefLookTime;
    float fishLookTime;

    public float move_speed = 2f;
    public bool can_jump = true;
    float jump_height = 500f;

    public Camera cam;

    public enum T_Snail
    {
        S_ONE,
        S_TWO,
        S_THREE
    };

    public List<int> s_list = new List<int>();
    public string name;

    void Grab()
    {
        print("grab!");
        grabbing = false;

        // As long as the snails aren't clustered this is fine

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, grabRange))
            if (hit.collider.gameObject.tag.Equals("Snail"))
            {
                print("got " + hit.collider.gameObject.name + "!");
                snailsCaught++;
                if (hit.collider.gameObject.GetComponent("Snail") != null)
                    s_list.Add(hit.collider.gameObject.GetComponent<Snail>().id);
                Destroy(GameObject.Find(hit.collider.gameObject.name));
            }
        /*
		GameObject[] snails = GameObject.FindGameObjectsWithTag ("Snail");
		foreach (GameObject s in snails) {
			if (isFacing (s) && isNear (s)) {
				print ("got " + s.name + "!");
				snailsCaught++;
				Destroy(GameObject.Find (s.name));
			}
		}
        */
    }

    bool isFacing(GameObject target)
    {
        Vector3 relativePos = target.transform.position - this.transform.position;
        float dot = Vector3.Dot(relativePos, Camera.main.transform.forward);
        return (dot > 0);
    }

    bool isNear(GameObject target)
    {
        float dist = Vector3.Distance(target.transform.position, this.transform.position);
        return (dist <= grabRange);
    }

    // Use this for initialization
    void Start()
    {
        startSnailTimer = true;
        hit = new RaycastHit();
    }

    // Update is called once per frame
    void Update()
    {
        // look
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, lookRange))
        {
            Debug.DrawRay(transform.position, Camera.main.transform.forward * lookRange, Color.red);
            if (hit.collider.gameObject.name == "Fish")
                fishLookTime += Time.deltaTime;
            if (hit.collider.gameObject.name == "Lobster")
                lobsterLookTime += Time.deltaTime;
            if (hit.collider.gameObject.tag == "Snail")
                snailLookTime += Time.deltaTime;
            if (hit.collider.gameObject.tag == "Reef")
                reefLookTime += Time.deltaTime;
        }
        if (startSnailTimer)
        {
            snailTimer += Time.deltaTime;
            timeStopped = snailTimer - timeMoving;
        }
        if (startSnailTimer && snailsCaught == 2)
        {
            startSnailTimer = false;
            print("You caught the snails in " + (int)snailTimer + " seconds!");
            print("Time Moving: " + timeMoving + " seconds!");
            print("Time Stopped: " + timeStopped + " seconds!");
            print("Fish Look Time: " + fishLookTime + " seconds!");
            print("Lobster Look Time: " + lobsterLookTime + " seconds!");
            print("Snail Look Time: " + snailLookTime + " seconds!");
            print("Reef Look Time: " + reefLookTime + " seconds!");

            // upload to analytics
            Analytics.CustomEvent("timeData", new Dictionary<string, object> {
                { "Snail Catch Time", snailTimer},
                { "Move Time", timeMoving},
                { "Stop Time", timeStopped},
                { "Fish Look Time", fishLookTime},
                { "Lobster Look Time", lobsterLookTime},
                { "Snail Look Time", snailLookTime},
                { "Reef Look Time", reefLookTime},
            });
        }

        // timer for double tap
        if (startTapTimer)
        {
            tapTimer += Time.deltaTime;
        }
        if (tapTimer > 0.5f)
        {
            startTapTimer = false;
            tapTimer = 0;
        }

        // movement
        /*
		if (Input.GetMouseButton(0)) {
			timeMoving += Time.deltaTime;
			Vector3 targetDirection = this.transform.forward;
		    targetDirection = Camera.main.transform.TransformDirection (targetDirection);
			targetDirection.y = 0.0f;
			this.transform.position += targetDirection * Time.deltaTime;
		}
        */

        transform.position += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * move_speed;
        transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * move_speed;

        if (Input.GetButtonDown("Jump") && can_jump)
            GetComponent<Rigidbody>().velocity = transform.up * jump_height;

        // claw
        if (Input.GetMouseButtonDown(0))
        {
            startTapTimer = true;
            if (tapTimer > 0f && tapTimer <= 0.5f && !grabbing)
            {
                Invoke("Grab", 1f);
                grabbing = true;
                startTapTimer = false;
                tapTimer = 0;
            }
        }
    }
}
