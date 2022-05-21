using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoam : MonoBehaviour
{
    /*
     * 카메라 자유시점 이동을 위한 스크립트
     */
    public GameObject selectedHero;

    public float camSpeed = 20;
    public float screenSizeThickness = 10;
    public float minY = 10f;
    public float maxY = 30.5f;
    public int _mask;

    public Vector2 screenLimit;

    void Update()
    {
        Vector3 pos = transform.position;

        // Up
        if(Input.mousePosition.y >= Screen.height - screenSizeThickness)
        {
            pos.z += camSpeed * Time.deltaTime;
        }

        // Down
        if (Input.mousePosition.y <= screenSizeThickness)
        {
            pos.z -= camSpeed * Time.deltaTime;
        }

        // Right
        if (Input.mousePosition.x >= Screen.height - screenSizeThickness)
        {
            pos.x += camSpeed * Time.deltaTime;
        }

        // Left
        if (Input.mousePosition.x <= screenSizeThickness)
        {
            pos.x -= camSpeed * Time.deltaTime;
        }


        // Return Camera to Player
        if (Input.GetKey(KeyCode.Space))
        {
            pos.x = selectedHero.transform.position.x;
            pos.z = selectedHero.transform.position.z - 13;
        }

        // Limit Camera Move
        pos.x = Mathf.Clamp(pos.x, -screenLimit.x, screenLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -screenLimit.y, screenLimit.y - 10);

        transform.position = pos;
    }

    public void Initialize(GameObject player)
    {
        selectedHero = player;

        if (selectedHero.layer == LayerMask.NameToLayer("RedTeam"))
            _mask = LayerMask.GetMask("BlueTeam");
        else
            _mask = LayerMask.GetMask("RedTeam");
    }
}
