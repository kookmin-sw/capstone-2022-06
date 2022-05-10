using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Marker : UI_Base
{
    enum GameObjects
    {
        Marker
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        transform.position = transform.parent.position + new Vector3(0, 9, 0);
        // SetMarkerColor(new Vector3(255, 0, 0));
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    void SetMarkerColor(Vector3 color)
    {
        GameObject marker = Get<GameObject>((int)GameObjects.Marker);
        Image markerImage = marker.GetComponent<Image>();
        markerImage.color = new Color32((byte)color.x, (byte)color.y, (byte)color.z, 255);
    }
}
