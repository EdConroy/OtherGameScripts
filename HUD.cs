using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    Vector2 mouseAbs;
    Vector2 mouseSmooth;

    public Vector2 clampInDegrees = new Vector2(360, 180);
    public bool lockCursor;
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 smoothing = new Vector2(3, 3);
    public Vector2 targetDirection;
    public Vector2 playerDirection;

    public GameObject player;
    public float speed = 5f;

    // Use this for initialization
    void Start()
    {
        targetDirection = transform.localRotation.eulerAngles;
        if (player)
            playerDirection = player.transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetOrientation = Quaternion.Euler(targetDirection);
        Quaternion playerOrientation = Quaternion.Euler(playerDirection);

        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(smoothing.x * sensitivity.x, smoothing.y * sensitivity.y));

        mouseSmooth.x = Mathf.Lerp(mouseSmooth.x, mouseDelta.x, 1f / smoothing.x);
        mouseSmooth.y = Mathf.Lerp(mouseSmooth.y, mouseDelta.y, 1f / smoothing.y);

        mouseAbs += mouseSmooth;

        if (clampInDegrees.x < 360)
            mouseAbs.x = Mathf.Clamp(mouseAbs.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

        Quaternion xRotation = Quaternion.AngleAxis(-mouseAbs.y, targetOrientation * Vector3.right);
        transform.localRotation = xRotation;

        if (clampInDegrees.y < 360)
            mouseAbs.y = Mathf.Clamp(mouseAbs.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

        transform.localRotation *= targetOrientation;

        if (player)
        {
            Quaternion yRotation = Quaternion.AngleAxis(mouseAbs.x, player.transform.up);
            player.transform.localRotation = yRotation;
            player.transform.localRotation *= playerOrientation;
        }
        else
        {
            Quaternion yRotation = Quaternion.AngleAxis(mouseAbs.x, transform.InverseTransformDirection(Vector3.up));
            transform.localRotation *= yRotation;
        }
    }
}
