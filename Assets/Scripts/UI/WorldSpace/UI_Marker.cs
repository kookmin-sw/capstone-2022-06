using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

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
        StartCoroutine(WaitBindCoroutine());
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    IEnumerator WaitBindCoroutine()
    {
        yield return new WaitUntil(() => {
            return Get<GameObject>((int)GameObjects.Marker) != null;
        });

        SetMarkerColor();
    }

    public void SetMarkerColor()
    {
        GameObject marker = Get<GameObject>((int)GameObjects.Marker);
        Image markerImage = marker.GetComponent<Image>();

        if (transform.parent.gameObject.layer == LayerMask.NameToLayer("RedTeam"))
        {
            markerImage.color = Color.red;
        }
        else
        {
            markerImage.color = Color.blue;
        }
    }
}
