using UnityEngine;
using System.Collections;

public class ChargeColider : MonoBehaviour {
    public int charge=0;
    public Vector3 Direction;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision c)
    {
       c.gameObject.SendMessage("setCharge",charge);
    }
    public int getCharge()
    {
        return charge;
    }


}
