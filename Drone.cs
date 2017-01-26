using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Enemy
{
    public UnityEngine.AI.NavMeshAgent agent;

    public void Pursuit(Collider c)
    {
        goal = c.gameObject.transform;
        agent.destination = goal.position;

        if (agent.remainingDistance < 0.5f)
        {
            Quaternion rotationOrigin = goal.rotation;

            transform.RotateAround(goal.position, Vector3.up, speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationOrigin, 1f);
        }
    }
}
