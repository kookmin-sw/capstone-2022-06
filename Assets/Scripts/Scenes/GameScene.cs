using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PhotonHash = ExitGames.Client.Photon.Hashtable;

public class GameScene : BaseScene
{
    PhotonHash myHash;

    void Start()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["PlayerReady"]);
    }

    public override void Clear() {}
}
