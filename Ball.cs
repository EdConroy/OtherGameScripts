using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int owner_id = 1;
    public Player owner;
    public Player opponent;
    public Rigidbody self;
    public float magnitude;
    public bool called = false;
	// Use this for initialization
	void Start ()
    {
        self = GetComponent<Rigidbody>();
        if (owner_id == 1)
        {
            owner = GameObject.Find("Player1").GetComponent<Player>();
            opponent = GameObject.Find("Player2").GetComponent<Player>();
        }
        else if(owner_id == 2)
        {
            owner = GameObject.Find("Player2").GetComponent<Player>();
            opponent = GameObject.Find("Player1").GetComponent<Player>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        magnitude = self.velocity.magnitude;
	}
    void ClearBoard()
    {
        DestroyObject(this.gameObject);
        called = false;
        owner.thrown = false;
    }
    void OnTriggerStay(Collider c)
    {
        if(c.gameObject.CompareTag("JackArea") && owner.set_jack && self.velocity.magnitude <= 0f)
        {
            owner.jack_pos = this.gameObject.transform.position;
            opponent.jack_pos = this.gameObject.transform.position;
            owner.set_jack = false;
            owner.turn = true;
            ClearBoard();
        }
        else if(c.gameObject.CompareTag("JackArea") && !owner.set_jack && self.velocity.magnitude <= 0f && !called)
        {
            owner.ball_pos = transform.position;
            owner.ball_locations.Add(owner.DistanceToJack(owner.jack_pos, owner.ball_pos));
            opponent.opp_ball_locations.Add(opponent.DistanceToJack(owner.jack_pos, owner.ball_pos));
            owner.attempts++;
            owner.turn = false;
            opponent.turn = true;
            ClearBoard();
        }
    }
}
