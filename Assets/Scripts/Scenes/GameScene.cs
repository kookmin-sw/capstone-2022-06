using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameScene : BaseScene
{
    int isCommander;
    int myId;

    void Start()
    {
        isCommander = (int)PhotonNetwork.LocalPlayer.CustomProperties["isCommander"];
        myId = (int)PhotonNetwork.LocalPlayer.CustomProperties["matchId"];

        // PritMyProp();
    }

    public override void Clear() {}

    private void PrintMyProp()
    {
        Debug.Log($"Status of commander {isCommander}");
        Debug.Log($"Status of local id {myId}");
    }
}
