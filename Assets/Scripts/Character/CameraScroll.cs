using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ¸¶¿ì½º ÈÙ »ç¿ë ½Ã Ä«¸Þ¶óÀÇ ÁÜÀÎ/ÁÜ¾Æ¿ô
 */

public class CameraScroll : MonoBehaviour
{
    public Camera cam;
    private float camFOV;
    public float zoomSpeed;

    private float mouseScrollInput;

    void Start()
    {
        camFOV = cam.fieldOfView;
    }

    void Update()
    {
        mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");

        camFOV -= mouseScrollInput * zoomSpeed;
        camFOV = Mathf.Clamp(camFOV, 30, 70);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, camFOV, zoomSpeed);
    }
}
