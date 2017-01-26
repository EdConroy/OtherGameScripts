using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
    private double levelTimer=300d;
	public static int currentLevel = 0;
	void Update () 
    {
        Debug.Log(levelTimer-=Time.deltaTime);
        if (levelTimer<=0)
            Application.LoadLevel(1);
        currentLevel=1;
	}
}
