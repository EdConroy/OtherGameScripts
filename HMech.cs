using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMech : Enemy
{
    public void Pursuit(Vector3 playerPos, UnityEngine.AI.NavMeshAgent agent, 
        float x_offset, float y_offset, float z_offset)
    {
        playerPos = new Vector3(
    player.transform.position.x + x_offset,
    player.transform.position.y + y_offset,
    player.transform.position.z + z_offset);

        agent.destination = playerPos;
    }
    public void HMechAttack()
    {
        //Attack the player
    }
    public void HMechAnimate()
    {
        //Preform animations based on the current state the Enemy is in
    }
}
