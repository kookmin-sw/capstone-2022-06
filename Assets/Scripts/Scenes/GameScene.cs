using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using photonHash = ExitGames.Client.Photon.Hashtable;

public class GameScene : BaseScene
{
    bool isCommander;
    int myId;

    void Start()
    {
        isCommander = (bool)PhotonNetwork.LocalPlayer.CustomProperties["isCommander"];
        myId = (int)PhotonNetwork.LocalPlayer.CustomProperties["actorId"];

        // PrintMyProp();
    }

    protected override void Init()
    {
        base.Init();

        sceneType = Define.Scene.Game;
        Managers.UI.ShowSceneUI<UI_GameScene>();
        
        // 게임 시작 시간을 커스텀 프로퍼티로 저장합니다.
        // 마스터만 저장하도록 강제합니다.
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new photonHash() {
                {"startTimestamp", PhotonNetwork.Time}
            });
        }
    }

    public override void Clear() {}

    private void PrintMyProp()
    {
        Debug.Log($"Status of commander {isCommander}");
        Debug.Log($"Status of local id {myId}");
    }
}
