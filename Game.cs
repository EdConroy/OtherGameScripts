using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Player p1, p2;
    public Text p_score;
    public Text o_score;
    public Text c_turn;
    public Text t_jack;

    public Camera p1_camera;
    public Camera p2_camera;

    public Button button;
    public bool active = false;

    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
	// Update is called once per frame
	void Update ()
    {
        if(active)
        {
            SetUI(p1, p2);
            SetCamera(p1, p2);
        }
        else if(!active)
        {
            c_turn.text = "";
            t_jack.text = "";
            p_score.text = "";
            o_score.text = "";
        }
	}
    void SetUI(Player one, Player two)
    {
        if (one.turn)
            c_turn.text = "Player 1";
        else if (!one.turn)
            c_turn.text = "Player 2";

        if (one.set_jack && !two.set_jack)
            t_jack.text = "Player 1 Setting Jack";
        else if (!one.set_jack && two.set_jack)
            t_jack.text = "Player 2 Setting Jack";
        else if (!one.set_jack && !two.set_jack)
            t_jack.text = "Jack is Set!";

        p_score.text = "Player 1: " + one.score.ToString();
        o_score.text = "Player 2: " + two.score.ToString();
    }
    void SetCamera(Player one, Player two)
    {
        if (!one.turn && two.turn)
        {
            p1_camera.enabled = false;
            p2_camera.enabled = true;
        }
        else if (one.turn && !two.turn)
        {
            p1_camera.enabled = true;
            p2_camera.enabled = false;
        }
    }
    void TaskOnClick()
    {
        if (active) active = false;
        else if (!active) active = true;
    }
}
