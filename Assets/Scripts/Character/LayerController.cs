using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LayerController : MonoBehaviour
{
    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public void SetLayer(string layerName)
    {
        PV.RPC("_SetLayer", RpcTarget.All, layerName);
    }

    [PunRPC]
    void _SetLayer(string layerName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }
}
