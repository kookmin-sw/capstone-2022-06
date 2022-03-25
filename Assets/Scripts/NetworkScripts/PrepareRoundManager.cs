using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class PrepareRoundManager : MonoBehaviourPunCallbacks
{
    string clickImgName;

    [SerializeField] List<GameObject> playersPick;  // �÷��̾� ��â ����Ʈ
    [SerializeField] Button confirmBtn;             // �÷��̾� ���� Ȯ�� ��ư
    [SerializeField] GameObject scrollView;         // è�Ǿ� ����â ��ũ�� ��

    PhotonView PV;
    int playerNum = 0;                              // PlayerList �ȿ����� ������ ����

    bool completeSelect;

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
       
        // PV = PhotonNetwork.Instantiate("Prefabs/@PV", new Vector3(0, 0, 0), Quaternion.identity).GetComponent<PhotonView>();
        PV = GetComponent<PhotonView>();

        confirmBtn.interactable = false;
        completeSelect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (clickImgName != null)
            PV.RPC("ChangeIcon", RpcTarget.All, clickImgName, playerNum);
    }

    // Ŭ���� è�Ǿ��� ������ �̹��� �̸��� ������
    public void findClickImg()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;

        clickImgName = clickObj.GetComponent<Image>().sprite.name;

        if (!confirmBtn.interactable)
            confirmBtn.interactable = true;

        Debug.Log(PhotonNetwork.PlayerList[0].ActorNumber);
    }

    // è�Ǿ��� �����ϴ� ��ư�� Ŭ�� �� �۵�
    public void onClickConfirm()
    {
        confirmBtn.gameObject.SetActive(false);
        scrollView.SetActive(false);
    }

    // �÷��̾ � è�Ǿ��� �����ߴ��� �������� �ٸ� Ŭ���̾�Ʈ������ ������Ʈ �� �� �ֵ���
    // RPC�� �Լ��� ȣ�� (in Update)
    [PunRPC]
    void ChangeIcon(string name, int playerNum)
    {
        Sprite iconImg = Managers.Resource.Load<Sprite>($"Private/Textures/Character Portraits/{name}");

        playersPick[playerNum].transform.Find("Portrait").GetComponent<Image>().sprite = iconImg;
    }


}
