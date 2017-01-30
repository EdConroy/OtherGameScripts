using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Enemy
{
    public void Pursuit(Collider c)
    {
        //FIX ME: Current iteration works but causeses a significant drop
        //in frame rate.

        Vector3 rotationPoint = new Vector3(
            goal.position.x + 2f, 
            goal.position.y + 2f, 
            goal.transform.position.z + 2f);

            Quaternion rotationOrigin = goal.rotation;

            transform.RotateAround(rotationPoint, Vector3.up, speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationOrigin, 1f);
    }
}
