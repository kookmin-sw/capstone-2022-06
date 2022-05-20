using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UI_EndPopup : UI_Base
{
    enum Texts
    {
        ResultText
    }

    public override void Init()
    {
        Bind<Text>(typeof(Texts));

        Util.SearchChild<Button>(gameObject).onClick.AddListener(() => {
            PhotonNetwork.LoadLevel(0);
        });
    }

    public void SetResultText(string s)
    {
        GetText((int)Texts.ResultText).text = s;
    }
}
