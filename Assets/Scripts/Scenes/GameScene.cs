using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    private void PrintMyProp()
    {
        Debug.Log($"Status of commander {isCommander}");
        Debug.Log($"Status of local id {myId}");
    }
}
