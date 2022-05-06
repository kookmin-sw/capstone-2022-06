using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UI_GameScene : UI_Scene
{
    enum Texts
    {
        TimeText
    }

    void Update()
    {
        object stamp;
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("startTimestamp", out stamp))
        {
            float elapseTime = Time.time - (float)stamp;
            GetText((int)Texts.TimeText).text = elapseTime.ToString();
        }
    }

    public override void Init()
    {
        base.Init();

        Bind<Text>(typeof(Texts));
    }
}
