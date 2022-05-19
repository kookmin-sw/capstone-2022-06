using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

// �÷��̾� ���� ü�¹ٰ� �׻� ȭ�鿡 �����ǰ� ���̴� ��ũ��Ʈ

public class PlayerHealthSlider : MonoBehaviour, IPunObservable
{
    [SerializeField] Slider slider;
    ChampionStat stat;
    PhotonView PV;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        stat = transform.parent.gameObject.GetComponent<ChampionStat>();   
    }

    void Update()
    {
        if (PV.IsMine)
            PV.RPC("SetSliderValue", RpcTarget.All, stat.Status.hp / stat.Status.maxHp);   
    }

    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation = Camera.main.transform.rotation;
    }

    [PunRPC]
    public void SetSliderValue(float hp)
    {
        slider.value = hp;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(slider.value);
        }
        else
        {
            stream.ReceiveNext();
        }
    }
}
