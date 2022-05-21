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

    void Start()
    {
        SetLayer(LayerMask.LayerToName(gameObject.layer));
    }

    /// </summary>
    /// 레이어를 설정합니다.
    /// 겸사겸사 자기 팀 레이어가 아니면 OffRenderer를 호출합니다. 자기 팀 레이어면 FieldOfView를 부착합니다.
    /// </summary>
    public void SetLayer(string layerName)
    {
        if (PV.IsMine)
        {
            int id = PV.ViewID;
            PV.RPC("_SetLayer", RpcTarget.All, id, layerName);
        }
    }

    /// <summary>
    /// 레이어를 진짜 설정하는 RPC 메서드
    /// </summary>
    [PunRPC]
    void _SetLayer(int id, string layerName)
    {
        PhotonView.Find(id).gameObject.layer = LayerMask.NameToLayer(layerName);
        if (PhotonView.Find(id).gameObject.GetComponent<FieldOfView>() == null)
        {
            AttachFovOrDisable(PhotonView.Find(id).gameObject);
        }
    }

    /// <summary>
    /// 해당 게임오브젝트의 레이어 마스크에 따라 FOV를 붙이거나 OffRenderer를 호출하는 메서드
    /// </summary>
    public void AttachFovOrDisable(GameObject go)
    {
        int myLayer = Util.GetMyLayer();
    
        if (go.layer == myLayer)
        {
            GameObject filter = Managers.Resource.Instantiate("ViewVisualisation", transform);
            FieldOfView fov = gameObject.GetOrAddComponent<FieldOfView>();
            fov.viewMeshFilter = filter.GetComponent<MeshFilter>();

            fov.allyMask = 1 << myLayer;
            if (myLayer == LayerMask.NameToLayer("BlueTeam"))
            {
                fov.opposingMask = LayerMask.GetMask("RedTeam");
            }
            else
            {
                fov.opposingMask = LayerMask.GetMask("BlueTeam");
            }
        }
        else
        {
            Util.OffRenderer(go.transform);
        }
    }
}
