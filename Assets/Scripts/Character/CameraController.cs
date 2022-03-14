using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 2000f;
    public float minY = 10f;
    public float maxY = 30.5f;

    public Vector2 panLimit;
    public GameObject player;


    void Update()
    {
        // Camera position
        Vector3 pos = transform.position;

        // Move by mousePoint
        if(Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        // Return Camera to Player
        if(Input.GetKey(KeyCode.Space))
        {
            pos.x = player.transform.position.x;
            pos.z = player.transform.position.z - 13;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * Time.deltaTime * 10000f;

        // Limit Camera Move
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y-10);

        transform.position = pos;
    }
}
