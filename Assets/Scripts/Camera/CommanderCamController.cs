using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderCamController : MonoBehaviour
{
    public float scrollSpeed = 50f;
    Camera myCam;

    void Start()
    {
        myCam = GetComponent<Camera>();
    }

    void Update()
    {
        float mouseY = Input.mousePosition.y;
        float mouseX = Input.mousePosition.x;

        Vector3 dir = new Vector3(0, 0, 0);

        if (mouseX <= Screen.width * 0.05)
        {
            dir.x = -1;
        }

        if (mouseX >= Screen.width * 0.95)
        {
            dir.x = 1;
        }

        if (mouseY >= Screen.height * 0.95)
        {
            dir.z = 1;
        }

        if (mouseY <= Screen.height * 0.05)
        {
            dir.z = -1;
        }

        myCam.transform.Translate(dir * Time.deltaTime * scrollSpeed, Space.World);
        Vector3 pos = myCam.transform.position;
        bool bound = false;

        if (myCam.transform.position.x < -105)
        {
            bound = true;
            pos.x = -105;
        }
        else if (myCam.transform.position.x > 105)
        {
            bound = true;
            pos.x = 105;
        }

        if (myCam.transform.position.z < -105)
        {
            bound = true;
            pos.z = -105;
        }
        else if (myCam.transform.position.z > 105)
        {
            bound = true;
            pos.z = 105;
        }

        if (bound)
        {
            myCam.transform.position = pos;
        }
    }
}
