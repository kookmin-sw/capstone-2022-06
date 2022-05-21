using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UI_EndPopup : UI_Scene
{
    enum Texts
    {
        ResultText
    }

    enum Buttons
    {
        LobbyButton
    }

    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.LobbyButton).onClick.AddListener(() => {
            PhotonNetwork.LeaveRoom();
        });
    }

    public void SetResultText(string s)
    {
        StartCoroutine(WaitBindTextCoroutine(s));
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }

    IEnumerator WaitBindTextCoroutine(string s)
    {
        yield return new WaitUntil(() => {
            return GetText((int)Texts.ResultText) != null;
        });

        if (s == Util.GetMyLayerString())
        {
            GetText((int)Texts.ResultText).text = "패배";
        }
        else
        {
            GetText((int)Texts.ResultText).text = "승리";
        }
    }
}
