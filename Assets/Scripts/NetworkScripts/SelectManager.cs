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
    int playerNum = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (p.UserId == PhotonNetwork.LocalPlayer.UserId)
                break;
            playerNum++;
        }

        // PV = PhotonNetwork.Instantiate("Prefabs/@PV", new Vector3(0, 0, 0), Quaternion.identity).GetComponent<PhotonView>();
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clickImgName != null)
            PV.RPC("ChangeIcon", RpcTarget.All, clickImgName, playerNum);
    }

    public void findClickImg()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;

        clickImgName = clickObj.GetComponent<Image>().sprite.name;

        Debug.Log(PhotonNetwork.PlayerList[0].ActorNumber);
    }

    [PunRPC]
    void ChangeIcon(string name, int playerNum)
    {
        Sprite iconImg = Managers.Resource.Load<Sprite>($"Private/Textures/Character Portraits/{name}");

        playersPick[playerNum].transform.Find("Portrait").GetComponent<Image>().sprite = iconImg;
    }
}
