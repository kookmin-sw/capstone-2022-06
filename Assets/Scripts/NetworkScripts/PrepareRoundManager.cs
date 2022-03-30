using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PrepareRoundManager : MonoBehaviourPunCallbacks
{
    string clickImgName;

    [SerializeField] List<GameObject> playersPick;  // 플레이어 픽창 리스트
    [SerializeField] Button confirmBtn;             // 플레이어 선택 확정 버튼
    [SerializeField] GameObject scrollView;         // 챔피언 선택창 스크롤 뷰

    PhotonView PV;
    int playerNum = 0;                              // PlayerList 안에서의 본인의 순서

    int readyPlayer = 0;

    public Hashtable _playerCustomProperties = new Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        // PlayerList 를 순회하면서 본인의 playerNum을 탐색
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (p.UserId == PhotonNetwork.LocalPlayer.UserId)
                break;
            playerNum++;
        }

        PV = GetComponent<PhotonView>();

        confirmBtn.interactable = false;

        PhotonNetwork.LocalPlayer.SetCustomProperties(_playerCustomProperties);
        _playerCustomProperties["PlayerReady"] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (clickImgName != null)
            PV.RPC("ChangeIcon", RpcTarget.All, clickImgName, playerNum);

        Debug.Log($"playerCount : {PhotonNetwork.PlayerList.Length}");

        if (readyPlayer < PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                Debug.Log($"player user id : {player.ActorNumber},  {player.CustomProperties["PlayerReady"]}");
                object temp;
                if (player.CustomProperties.TryGetValue("PlayerReady", out temp))
                {
                    Debug.Log($"num : {playerNum}, ready : {(int)temp}");
                    readyPlayer += (int)temp;
                    player.CustomProperties["PlayerReady"] = 0;
                }
                else
                {
                    Debug.Log($"num : {playerNum}, Not valid value");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
            Debug.Log($"readyPlayer : {readyPlayer}");
    }

    // 클릭한 챔피언의 아이콘 이미지 이름을 가져옴
    public void findClickImg()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;

        clickImgName = clickObj.GetComponent<Image>().sprite.name;

        if (!confirmBtn.interactable)
            confirmBtn.interactable = true;
    }

    // 챔피언을 선택하는 버튼을 클릭 시 작동
    public void onClickConfirm()
    {
        confirmBtn.gameObject.SetActive(false);
        scrollView.SetActive(false);

        if (!PhotonNetwork.IsMasterClient)
            PV.RPC("UpdateReadyCustomProperties", RpcTarget.All, 1);
        else
            PhotonNetwork.LocalPlayer.CustomProperties["PlayerReady"] = 1;

        if (!PhotonNetwork.IsMasterClient) return;
        
        StartCoroutine(WaitAllReady());
    }

    private IEnumerator WaitAllReady()
    {
        yield return new WaitUntil(() => readyPlayer == PhotonNetwork.CurrentRoom.MaxPlayers);

        Debug.Log($"readyPlayer : {readyPlayer}");
        PhotonNetwork.LoadLevel(2);
    }

    // 플레이어가 어떤 챔피언을 선택했는지 아이콘을 다른 클라이언트에서도 업데이트 할 수 있도록
    // RPC로 함수를 호출 (in Update)
    [PunRPC]
    void ChangeIcon(string name, int playerNum)
    {
        Sprite iconImg = Managers.Resource.Load<Sprite>($"Private/Textures/Character Portraits/{name}");

        playersPick[playerNum].transform.Find("Portrait").GetComponent<Image>().sprite = iconImg;
    }

    [PunRPC]
    void UpdateReadyCustomProperties(int ready)
    {
        PhotonNetwork.LocalPlayer.CustomProperties["PlayerReady"] = ready;
    }
}
