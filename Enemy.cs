using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    enum enemyClass{T_HMECH, T_PROTECTOR, T_DRONE, T_KAMIKAZE, T_DPS, T_HEALER, T_CALLY, T_CENEMY};

    public int enemy_id;
    public Transform[] routes;
    public Transform goal;
    private int destRoute = 0;
    private UnityEngine.AI.NavMeshAgent agent;

    public bool alerted = false;
    private bool called = false;
    public bool healing = false;
    private bool acting = false;
    public int spotted = 0;

    public GameObject player;
    public GameObject p_flank;
    private Vector3 playerPos;
    public float speed;

    public float x_offset, y_offset, z_offset;

    public Enemy[] enemyList;
    public Enemy[] cinematicList;

    public int health = 100;
    public GameObject self;

    void Start()
    {
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

        if(acting)
        {
            int c_enemy_count = 0;
            for(int i = 0; i < cinematicList.Length; i++)
            {
                if (cinematicList[i].enemy_id == (int)enemyClass.T_CENEMY)
                    c_enemy_count++;
            }
            if (c_enemy_count <= 0)
                acting = false;
        }
    }

    void Pursuit()
    {
        //TODO: implement a genric chase function for the enemy character
        alerted = true;
        goal = player.transform;
        agent.destination = goal.position;
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
                goal = null;
                GoToNextPoint();
            }
        }

        if(enemy_id == (int) enemyClass.T_HEALER)
        {
            healing = true;
            Healer h = new Healer();
            h.FindPatients(enemyList, agent);
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
            if (spotted >= 8 && enemy_id == (int) enemyClass.T_KAMIKAZE)
            {
                Pursuit();

                //TODO: Explode the player
            }
            if(spotted >= 8 && enemy_id == (int) enemyClass.T_DPS)
            {
                alerted = true;
                DPS d = new DPS();
                d.Pursuit(agent, p_flank);

                //TODO: Slice and Dice the player
            }
            if(spotted >= 8 && enemy_id == (int) enemyClass.T_HMECH || enemy_id == (int) enemyClass.T_PROTECTOR)
            {
                alerted = true;
                HMech h = new HMech();
                h.Pursuit(playerPos, agent, x_offset, y_offset, z_offset);
                
                //TODO: Shoot at the player
            }
            if(spotted >= 8 && enemy_id == (int) enemyClass.T_DRONE)
            {
                alerted = true;
                Drone d = new Drone();
                d.agent = agent;
                goal = c.gameObject.transform;
                d.Pursuit(c);
                
                //TODO: Do whatever it is that drones do
            }
            if(spotted >= 8 && enemy_id == (int) enemyClass.T_HEALER)
            {
                alerted = true;
                Healer h = new Healer();
                h.Pursuit(playerPos, agent, x_offset, y_offset, z_offset);
            }
        }
        if(c.gameObject.GetComponent("Enemy") != null && 
            (enemy_id == (int) enemyClass.T_CALLY || 
            enemy_id == (int) enemyClass.T_CENEMY))
        {
            acting = true;
            if(c.gameObject.CompareTag("Cinematic"))
            {
                if (goal)
                    agent.destination = goal.position;
                else if (!goal)
                    return;

                float rand = Random.Range(1f, 100f);

                if(enemy_id == (int) enemyClass.T_CENEMY && rand % 7f == 0)
                {
                    DestroyImmediate(this.gameObject);
                }
            }
        }
    }
}
