using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    int enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.layer == LayerMask.NameToLayer("RedTeam"))
            enemyLayer = LayerMask.NameToLayer("BlueTeam");
        else
            enemyLayer = LayerMask.NameToLayer("RedTeam");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseCursor();
    }

    void UpdateMouseCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits;

        hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            // if (hit.collider.gameObject.layer == enemyLayer && hit.collider == hit.collider.gameObject.GetComponent<CapsuleCollider>())
            if (hit.collider.gameObject.layer == enemyLayer)
            {
                Texture2D tex = Managers.Resource.Load<Texture2D>("Textures/Cursors/Cursor_Attack");
                Cursor.SetCursor(tex, new Vector2(tex.width / 5, 0), CursorMode.Auto);
                break;
            }
            else
            {
                Texture2D tex = Managers.Resource.Load<Texture2D>("Textures/Cursors/Cursor_Hand");
                Cursor.SetCursor(tex, new Vector2(tex.width / 3, 0), CursorMode.Auto);
            }
        }
    }
}
