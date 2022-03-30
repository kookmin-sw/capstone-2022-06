using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Marker : UI_Base
{
    public override void Init()
    {
        this.Bind<GameObject>(typeof(GameObjects));
        // SetMarkerColor(new Vector3(255, 0, 0));
    }

    void OnEnable()
    {
        Init();
    }

    void Update()
    {
        
    }

    void SetMarkerColor(Vector3 color)
    {
        GameObject marker = this.Get<GameObject>((int)GameObjects.Marker);
        Image markerImage = marker.GetComponent<Image>();
        markerImage.color = new Color32((byte)color.x, (byte)color.y, (byte)color.z, 0);
    }

    enum GameObjects
    {
        Marker
    }
}
