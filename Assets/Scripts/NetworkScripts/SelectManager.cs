using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class SelectManager : MonoBehaviourPunCallbacks
{
    string clickImgName;

    [SerializeField]
    List<GameObject> playersPick;

    PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clickImgName != null)
            PV.RPC("ChangeIcon", RpcTarget.All, clickImgName);
    }

    public void findClickImg()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;

        clickImgName = clickObj.GetComponent<Image>().sprite.name;
    }

    [PunRPC]
    void ChangeIcon(string name)
    {
        Sprite iconImg = Managers.Resource.Load<Sprite>($"Private/Textures/Character Portraits/{name}");

        playersPick[PV.ViewID - 1].transform.Find("Portrait").GetComponent<Image>().sprite = iconImg;
    }
}
