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

    /// </summary>
    /// 레이어를 설정합니다.
    /// 겸사겸사 자기 팀 레이어가 아니면 OffRenderer를 호출합니다.
    /// </summary>
    public void SetLayer(string layerName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("_SetLayer", RpcTarget.All, layerName);
        }

        int id = (int)PhotonNetwork.LocalPlayer.CustomProperties["actorId"];
        if ((id <= 5 && gameObject.layer == LayerMask.NameToLayer("RedTeam")) ||
        (id > 5 && gameObject.layer == LayerMask.NameToLayer("BlueTeam")))
        {
            Util.OffRenderer(transform);
        }
    }

    [PunRPC]
    void _SetLayer(string layerName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }
}
