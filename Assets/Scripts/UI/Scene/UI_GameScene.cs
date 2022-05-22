using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UI_GameScene : UI_Scene
{
    UI_ExitMenuPopup popup;

    enum Texts
    {
        TimeText
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!popup.gameObject.activeSelf)
            {
                popup.gameObject.SetActive(true);
            }
        }
    }

    void LateUpdate()
    {
        object stamp;
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("startTimestamp", out stamp))
        {
            double elapseTime = PhotonNetwork.Time - (double)stamp;
            int minutes = (int)elapseTime / 60;
            int seconds = (int)elapseTime % 60;
            GetText((int)Texts.TimeText).text = $"{minutes}:{seconds}";
        }
    }

    public override void Init()
    {
        base.Init();

        Bind<Text>(typeof(Texts));

        popup = Managers.UI.AttachSubItem<UI_ExitMenuPopup>(transform);
        popup.gameObject.SetActive(false);

        if ((bool)GetPropVal("isCommander"))
        {
            Managers.UI.AttachSubItem<UI_ComSkill>(transform);
        }
        else
        {
            Managers.UI.AttachSubItem<UI_ChampSkill>(transform);
        }
    }

    object GetPropVal(object key)
    {
        object ret;

        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(key, out ret))
        {
            return ret;
        }

        return null;
    }
}
