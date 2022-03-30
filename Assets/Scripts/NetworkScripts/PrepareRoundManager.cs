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

    [SerializeField] List<GameObject> playersPick;  // �÷��̾� ��â ����Ʈ
    [SerializeField] Button confirmBtn;             // �÷��̾� ���� Ȯ�� ��ư
    [SerializeField] GameObject scrollView;         // è�Ǿ� ����â ��ũ�� ��

    PhotonView PV;
    int playerNum = 0;                              // PlayerList �ȿ����� ������ ����

    int readyPlayer = 0;

    public Hashtable _playerCustomProperties = new Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        // PlayerList �� ��ȸ�ϸ鼭 ������ playerNum�� Ž��
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

    // Ŭ���� è�Ǿ��� ������ �̹��� �̸��� ������
    public void findClickImg()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;

        clickImgName = clickObj.GetComponent<Image>().sprite.name;

        if (!confirmBtn.interactable)
            confirmBtn.interactable = true;
    }

    // è�Ǿ��� �����ϴ� ��ư�� Ŭ�� �� �۵�
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

    // �÷��̾ � è�Ǿ��� �����ߴ��� �������� �ٸ� Ŭ���̾�Ʈ������ ������Ʈ �� �� �ֵ���
    // RPC�� �Լ��� ȣ�� (in Update)
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
