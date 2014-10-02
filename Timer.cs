using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
    private double levelTimer=300d;
	void Update () 
    {
        Debug.Log(levelTimer-=Time.deltaTime);
        if (levelTimer<=0)
            Application.LoadLevel(1);
        Tools.currentLevel=1;
	}
}
