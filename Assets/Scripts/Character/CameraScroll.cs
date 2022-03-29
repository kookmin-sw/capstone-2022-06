using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���콺 �� ��� �� ī�޶��� ����/�ܾƿ�
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
