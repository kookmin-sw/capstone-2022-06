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

    public override void Clear() {}

    protected override void Init()
    {
        base.Init();

        // 게임 시작 시간을 커스텀 프로퍼티로 저장합니다.
        // 마스터만 저장하도록 강제합니다.
        if (PhotonNetwork.IsMasterClient)
        {
            photonHash roomHash = PhotonNetwork.CurrentRoom.CustomProperties;
            roomHash.Add("startTimestamp", Time.time);
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
        }
    }

    private void PrintMyProp()
    {
        Debug.Log($"Status of commander {isCommander}");
        Debug.Log($"Status of local id {myId}");
    }
}
