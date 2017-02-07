﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public enum enemyClass{T_HMECH, T_PROTECTOR, T_DRONE, T_KAMIKAZE, T_DPS, T_HEALER, T_CALLY, T_CENEMY};

    public enemyClass enemy_id;

    public Transform[] routes;
    public Transform goal;
    private int destRoute = 0;
    private UnityEngine.AI.NavMeshAgent agent;

    protected bool alerted = false;
    protected bool called = false;
    protected bool healing = false;
    protected bool acting = false;
    protected bool rotating = false;
    private int spotted = 0;

    public GameObject player;
    public GameObject p_flank;
    private Vector3 playerPos;
    public float speed;

    public float x_offset, y_offset, z_offset;

    public static List<Enemy> enemyList;
    public static int c_enemy_count = 0;

    public int health = 100;
    public GameObject self;

    void Start()
    {
        if (enemy_id == enemyClass.T_CENEMY)
            ++c_enemy_count;
        if (enemy_id != enemyClass.T_CALLY || enemy_id != enemyClass.T_CENEMY)
            enemyList.Add(this);
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.autoBraking = false;
        if (!alerted)
            GoToNextPoint();
	}
	
    void GoToNextPoint()
    {
        called = true;
        if (routes.Length == 0)
            return;
        agent.destination = routes[destRoute].position;
        destRoute = (destRoute + 1) % routes.Length;
    }

    void Pursuit()
    {
        alerted = true;
        goal = player.transform;
        agent.destination = goal.position;
        //Change Animation State to alerted
    }

	void Update ()
    {
        if (agent.remainingDistance < 0.5f && !alerted && !called && !healing && !acting)
            GoToNextPoint();
        else if(goal && alerted)
            agent.destination = goal.position;
        called = false;

        if (spotted > 0)
            spotted--;
        else if (spotted <= 0)
        {
            spotted = 0;
            if (alerted)
            {
                alerted = false;
                rotating = false;
                goal = null;
                GoToNextPoint();
            }
        }

        //Make sure that we have valid patients before we check for them
        for (int i = 0; i < enemyList.Count; i++)
            if (enemyList[i].health <= 0)
                enemyList.RemoveAt(i);

        if (enemy_id == enemyClass.T_HEALER)
        {
            healing = true;
            Healer h = new Healer();
            h.FindPatients(enemyList, agent);
        }

        if (enemy_id == enemyClass.T_CALLY && acting)
        {
            if (c_enemy_count < 1)
            {
                goal = null;
                agent.autoBraking = false;
                acting = false;

                Debug.Log("Called");
                GoToNextPoint();
            }
        }

        if (agent.autoBraking && (!rotating || !acting))
            agent.autoBraking = false;
    }

    void LateUpdate()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);

        Debug.DrawRay(transform.position, Vector3.forward, Color.green);

        if (Physics.Raycast(ray, 100f))
        {
            if(enemy_id == enemyClass.T_DRONE)
            {
                rotating = true;
                alerted = true;
                Drone d = new Drone();
                d.agent = agent;
                goal = player.transform;
                d.Pursuit();
                agent.autoBraking = true;

                //TODO: Whatever it is that drones do
            }
        }
    }

    void OnTriggerStay(Collider c)
    {
        if(c.gameObject.GetComponent("Player") != null)
        {
            if (spotted >= 20)
                spotted = 20;
            else if (spotted <= 20)
                spotted++;
            if (spotted >= 8 && enemy_id == enemyClass.T_KAMIKAZE)
            {
                Pursuit();

                //TODO: Explode the player
                EnemySelfDestruct();
            }
            if(spotted >= 8 && enemy_id == enemyClass.T_DPS)
            {
                alerted = true;
                DPS d = new DPS();
                d.Pursuit(agent, p_flank);

                //TODO: Slice and Dice the player
            }
            if(spotted >= 8 && enemy_id == enemyClass.T_HMECH || enemy_id == enemyClass.T_PROTECTOR)
            {
                alerted = true;
                HMech h = new HMech();
                h.Pursuit(playerPos, agent, x_offset, y_offset, z_offset);
                
                //TODO: Shoot at the player
            }
            if(spotted >= 8 && enemy_id == enemyClass.T_HEALER)
            {
                alerted = true;
                Healer h = new Healer();
                h.Pursuit(playerPos, agent, x_offset, y_offset, z_offset);
            }
        }
        if(c.gameObject.GetComponent("Enemy") != null && 
            (enemy_id == enemyClass.T_CALLY || 
            enemy_id == enemyClass.T_CENEMY))
        {
            acting = true;

            if (c.gameObject.CompareTag("Cinematic"))
            {
                if (goal)
                {
                    agent.destination = goal.position;
                    agent.autoBraking = true;
                    //When it is a certain distance from the destination start shooting animation
                }
                else if (!goal)
                    return;

                if (enemy_id == enemyClass.T_CENEMY)
                {
                    health--;
                    if (health < 0)
                    {
                        c_enemy_count--;
                        //Play death animation then wait for a bit then destroy this
                        Destroy(this.gameObject);
                        
                    }
                }
            }

        }
    }
    void EnemySelfDestruct()
    {
        //When in a certain range of the player destroy me and damage the player
        //Also make explosion animation
    }
}
