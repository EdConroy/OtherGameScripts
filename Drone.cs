using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Enemy
{
    public void Pursuit()
    {
        //FIX ME: Current iteration works but causeses a significant drop
        //in frame rate.

        Vector3 rotationPoint = new Vector3(
            goal.position.x + 9f, 
            goal.position.y + 9f, 
            goal.transform.position.z + 9f);

            Quaternion rotationOrigin = goal.rotation;

            transform.RotateAround(rotationPoint, Vector3.up, speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationOrigin, 1f);
    }
    public void DroneAttack()
    {
        //Attack the player
    }
    public void DroneAnimate()
    {
        //Preform animations based on the current state the Enemy is in
    }
}
