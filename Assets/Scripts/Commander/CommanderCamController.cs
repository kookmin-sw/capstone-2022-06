using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderCamController : MonoBehaviour
{
    public float scrollSpeed = 15f;

    void Update()
    {
        float mouseY = Input.mousePosition.y;
        float mouseX = Input.mousePosition.x;
        if (mouseY <= Screen.height * 0.05 || mouseY >= Screen.height * 0.95 ||
        mouseX <= Screen.width * 0.05 || mouseX >= Screen.width * 0.95)
        {
            Vector3 dir = new Vector3(mouseX, Camera.main.transform.position.y, mouseY);
            Camera.main.transform.Translate(dir * Time.deltaTime * scrollSpeed, Space.World);
        }
    }
}
