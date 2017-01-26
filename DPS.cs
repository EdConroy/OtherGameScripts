using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPS : Enemy
{
    public void Pursuit(UnityEngine.AI.NavMeshAgent agent, GameObject p_flank)
    {
        goal = p_flank.transform;
        agent.destination = goal.position;
    }
}
