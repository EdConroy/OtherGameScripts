using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //Player 1 Data
    public Text money_total;
    public Text key_total;
    public Text ammo;

    public Image HealthBar1;
    public Image HealthBar2;
    public Image HealthBar3;

    public Image Picture;
    public Image AliveStatus;
    public Image Weapon;
    public Image Powerup;
    public Image PowerupCooldown;

    //Player 2 Data
    public Text p2_money_total;
    public Text p2_key_total;
    public Text p2_ammo;

    public Image P2HealthBar1;
    public Image P2HealthBar2;
    public Image P2HealthBar3;

    public Image P2Picture;
    public Image P2AliveStatus;
    public Image P2Money;
    public Image P2Keys;
    public Image P2Weapon;
    public Image P2Powerup;
    public Image P2PowerupCooldown;

    //Health Sprites
    public Sprite health_max;
    public Sprite health_half;
    public Sprite health_none;
    public Sprite dead;

    //Cooldown Sprites
    public Sprite cool_full;
    public Sprite cool_2_3;
    public Sprite cool_1_3;
    public Sprite cool_empty;

    //Players
    public Player p1;
    public Player p2;

    //Minimap
    public Transform Minimap;
    public Transform MinimapPosition;

    //Status Bar
    public GameObject status;
    public Text status_text;
    public List<Transform> status_pos = new List<Transform>();
    public List<GameObject> status_list = new List<GameObject>();
    public bool created = false;

    // Update is called once per frame
    void Update ()
    {
        SetUI(p1);
        if(p2) SetUI(p2, 2);
        else if (!p2)
        {
            p2_money_total.text = "";
            p2_key_total.text = "";
            p2_ammo.text = "";
            Destroy(P2HealthBar3);
            Destroy(P2HealthBar2);
            Destroy(P2HealthBar1);
            Destroy(P2Picture);
            Destroy(P2AliveStatus);
            Destroy(P2Money);
            Destroy(P2Keys);
            Destroy(P2Weapon);
            Destroy(P2Powerup);
            Destroy(P2PowerupCooldown);
            Minimap.position = MinimapPosition.position;
        }
    }
    void ClearStatus()
    {
        for(int i = 0; i < status_list.Count; i++)
        {
            status_list.RemoveAt(i);
            DestroyImmediate(status_list[i], true);
        }
        created = false;
        Debug.Log("Called");
    }
    void SetUI(Player p1)
    {
        switch (p1.health)
            {

            case 5:
                HealthBar3.sprite = health_half;
                break;
            case 4:
                HealthBar3.sprite = health_none;
                break;
            case 3:
                HealthBar2.sprite = health_half;
                break;
            case 2:
                HealthBar2.sprite = health_none;
                break;
            case 1:
                HealthBar1.sprite = health_half;
                break;
            case 0:
                HealthBar1.sprite = dead;
                HealthBar2.sprite = dead;
                HealthBar3.sprite = dead;
                break;
            default:
                HealthBar1.sprite = health_max;
                HealthBar2.sprite = health_max;
                HealthBar3.sprite = health_max;
                break;
        };

        if (p1.cooldown <= 74) PowerupCooldown.sprite = cool_2_3;
        if (p1.cooldown <= 49) PowerupCooldown.sprite = cool_1_3;
        if (p1.cooldown <= 0)  PowerupCooldown.sprite = cool_empty;

        money_total.text = p1.money.ToString();
        key_total.text = p1.keys.ToString();
        ammo.text = p1.ammo.ToString();

        if(p1.flag == NW_FLAGS.NW_PARALYZED && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Paralyzed";
            created = true;
        }
        if(p1.flag == NW_FLAGS.NW_POISION && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Poisioned";
            created = true;
        }
        if(p1.flag == NW_FLAGS.NW_SLEEP && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Sleep";
            created = true;
        }
        if(p1.flag == NW_FLAGS.NW_STUNNED && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Stunned";
            created = true;
        }
        if(p1.flag == NW_FLAGS.NW_DEAD && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Dead";
            created = true;
        }
        if(p1.flag == NW_FLAGS.NW_PARALYZED_SLEEP && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Paralyzed";
            Instantiate(status, status_pos[1]);
            status_list.Add(status);
            status_text.text = "Sleep";
            created = true;
        }
        if (p1.flag == NW_FLAGS.NW_PARALYZED_STUNNED && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Paralyzed";
            Instantiate(status, status_pos[1]);
            status_list.Add(status);
            status_text.text = "Stunned";
            created = true;
        }
        if (p1.flag == NW_FLAGS.NW_POISION_PARALYZED && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Poision";
            Instantiate(status, status_pos[1]);
            status_list.Add(status);
            status_text.text = "Paralyzed";
            created = true;
        }
        if (p1.flag == NW_FLAGS.NW_POISION_SLEEP && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Poision";
            Instantiate(status, status_pos[1]);
            status_list.Add(status);
            status_text.text = "Sleep";
            created = true;
        }
        if (p1.flag == NW_FLAGS.NW_POISION_STUNNED && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Poision";
            Instantiate(status, status_pos[1]);
            status_list.Add(status);
            status_text.text = "Stunned";
            created = true;
        }
        if (p1.flag == NW_FLAGS.NW_SLEEP_STUNNED && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Stunned";
            Instantiate(status, status_pos[1]);
            status_list.Add(status);
            status_text.text = "Sleep";
            created = true;
        }
        if (p1.flag == NW_FLAGS.NW_SLEEP_STUNNED_POISION && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Stunned";
            Instantiate(status, status_pos[1]);
            status_list.Add(status);
            status_text.text = "Sleep";
            Instantiate(status, status_pos[2]);
            status_list.Add(status);
            status_text.text = "Poision";
            created = true;
        }
        if (p1.flag == NW_FLAGS.NW_SLEEP_STUNNED_PARALYZED && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Stunned";
            Instantiate(status, status_pos[1]);
            status_list.Add(status);
            status_text.text = "Sleep";
            Instantiate(status, status_pos[2]);
            status_list.Add(status);
            status_text.text = "Paralyzed";
            created = true;
        }
        if (p1.flag == NW_FLAGS.NW_PARALYZED_SLEEP_POISION && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Paralyzed";
            Instantiate(status, status_pos[1]);
            status_list.Add(status);
            status_text.text = "Sleep";
            Instantiate(status, status_pos[2]);
            status_list.Add(status);
            status_text.text = "Poision";
            created = true;
        }
        if (p1.flag == NW_FLAGS.NW_PARALYZED_STUNNED_POISION && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Paralyzed";
            Instantiate(status, status_pos[1]);
            status_list.Add(status);
            status_text.text = "Stunned";
            Instantiate(status, status_pos[2]);
            status_list.Add(status);
            status_text.text = "Poision";
            created = true;
        }
        if (p1.flag == NW_FLAGS.NW_SLEEP_STUNNED_PARALYZED_POISIONED && !created)
        {
            ClearStatus();
            Instantiate(status, status_pos[0]);
            status_list.Add(status);
            status_text.text = "Stunned";
            Instantiate(status, status_pos[1]);
            status_list.Add(status);
            status_text.text = "Sleep";
            Instantiate(status, status_pos[2]);
            status_list.Add(status);
            status_text.text = "Poision";
            Instantiate(status, status_pos[3]);
            status_list.Add(status);
            status_text.text = "Paralyzed";
            created = true;
        }
    }
    void SetUI(Player p, int flag)
    {
        if (flag == 1) SetUI(p);
        else if(flag == 2)
        {
            switch (p.health)
            {
                case 5:
                    P2HealthBar3.sprite = health_half;
                    break;
                case 4:
                    P2HealthBar3.sprite = health_none;
                    break;
                case 3:
                    P2HealthBar2.sprite = health_half;
                    break;
                case 2:
                    P2HealthBar2.sprite = health_none;
                    break;
                case 1:
                    P2HealthBar1.sprite = health_half;
                    break;
                case 0:
                    P2HealthBar1.sprite = dead;
                    P2HealthBar2.sprite = dead;
                    P2HealthBar3.sprite = dead;
                    break;
                default:
                    P2HealthBar1.sprite = health_max;
                    P2HealthBar2.sprite = health_max;
                    P2HealthBar3.sprite = health_max;
                    break;
            };

            if (p.cooldown <= 74) P2PowerupCooldown.sprite = cool_2_3;
            if (p.cooldown <= 49) P2PowerupCooldown.sprite = cool_1_3;
            if (p.cooldown <= 0)  P2PowerupCooldown.sprite = cool_empty;

            p2_money_total.text = p.money.ToString();
            p2_key_total.text = p.keys.ToString();
            p2_ammo.text = p.ammo.ToString();

            if (p.flag == NW_FLAGS.NW_PARALYZED && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Paralyzed";
                created = true;
            }
            if (p.flag == NW_FLAGS.NW_POISION && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Poisioned";
                created = true;
            }
            if (p.flag == NW_FLAGS.NW_SLEEP && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Sleep";
                created = true;
            }
            if (p.flag == NW_FLAGS.NW_STUNNED && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Stunned";
                created = true;
            }
            if (p.flag == NW_FLAGS.NW_DEAD && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Dead";
                created = true;
            }
            if (p.flag == NW_FLAGS.NW_PARALYZED_SLEEP && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Paralyzed";
                Instantiate(status, status_pos[1]);
                status_text.text = "Sleep";
                created = true;
            }
            if (p.flag == NW_FLAGS.NW_PARALYZED_STUNNED && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Paralyzed";
                Instantiate(status, status_pos[1]);
                status_text.text = "Stunned";
                created = true;
            }
            if (p.flag == NW_FLAGS.NW_POISION_PARALYZED && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Poision";
                Instantiate(status, status_pos[1]);
                status_text.text = "Paralyzed";
                created = true;
            }
            if (p.flag == NW_FLAGS.NW_POISION_SLEEP && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Poision";
                Instantiate(status, status_pos[1]);
                status_text.text = "Sleep";
                created = true;
            }
            if (p.flag == NW_FLAGS.NW_POISION_STUNNED && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Poision";
                Instantiate(status, status_pos[1]);
                status_text.text = "Stunned";
                created = true;
            }
            if (p.flag == NW_FLAGS.NW_SLEEP_STUNNED && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Stunned";
                Instantiate(status, status_pos[1]);
                status_text.text = "Sleep";
                created = true;
            }
            if (p.flag == NW_FLAGS.NW_SLEEP_STUNNED_POISION && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Stunned";
                Instantiate(status, status_pos[1]);
                status_text.text = "Sleep";
                Instantiate(status, status_pos[2]);
                status_text.text = "Poision";
                created = true;
            }
            if (p.flag == NW_FLAGS.NW_SLEEP_STUNNED_PARALYZED && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Stunned";
                Instantiate(status, status_pos[1]);
                status_text.text = "Sleep";
                Instantiate(status, status_pos[2]);
                status_text.text = "Paralyzed";
                created = true;
            }
            if (p.flag == NW_FLAGS.NW_PARALYZED_SLEEP_POISION && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Paralyzed";
                Instantiate(status, status_pos[1]);
                status_text.text = "Sleep";
                Instantiate(status, status_pos[2]);
                status_text.text = "Poision";
                created = true;
            }
            if (p.flag == NW_FLAGS.NW_PARALYZED_STUNNED_POISION && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Paralyzed";
                Instantiate(status, status_pos[1]);
                status_text.text = "Stunned";
                Instantiate(status, status_pos[2]);
                status_text.text = "Poision";
                created = true;
            }
            if (p.flag == NW_FLAGS.NW_SLEEP_STUNNED_PARALYZED_POISIONED && !created)
            {
                Instantiate(status, status_pos[0]);
                status_text.text = "Stunned";
                Instantiate(status, status_pos[1]);
                status_text.text = "Sleep";
                Instantiate(status, status_pos[2]);
                status_text.text = "Poision";
                Instantiate(status, status_pos[3]);
                status_text.text = "Paralyzed";
                created = true;
            }
        }
    }
}
