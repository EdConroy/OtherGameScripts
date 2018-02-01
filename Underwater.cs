using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour
{

    public bool isUnderwater = false;
    private Color normalColor;
    private Color underwaterColor;
    public GameObject plane;
    public Texture text;
    public PlayerController player;

    // Use this for initialization
    void Start()
    {
        normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        underwaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag.Equals("WaterLevel"))
        {
            isUnderwater = true;
            RenderSettings.fogColor = underwaterColor;
            RenderSettings.fogDensity = 0.15f;
            player.can_jump = true;
            player.move_speed = 0.75f;
            Physics.gravity = new Vector3(0, -0.1f, 0);
        }
    }
    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag.Equals("WaterLevel"))
        {
            isUnderwater = false;
            RenderSettings.fogColor = normalColor;
            RenderSettings.fogDensity = 0.002f;
            player.can_jump = false;
            Physics.gravity = new Vector3(0, -9.81f, 0);

            plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            Destroy(plane.GetComponent<MeshCollider>());
            plane.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            plane.transform.parent = transform;
            plane.GetComponent<Renderer>().material.shader = Shader.Find("FX/Glass/StainedBumpDistort");
            plane.GetComponent<Renderer>().material.SetTexture("_BumpMap", text);
            plane.GetComponent<Renderer>().material.SetFloat("_BumpMap", 0);

            WaitForSeconds w = new WaitForSeconds(2);

            DestroyObject(plane);
        }
    }
}
