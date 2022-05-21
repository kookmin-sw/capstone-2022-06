using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TempleController : Controller, IPunObservable
{
    HQStat stat;
    bool broken = false;

    [SerializeField] Slider HPSlider;

    // Start is called before the first frame update
    void Start()
    {
        stat = GetComponent<HQStat>();
        stat.Initialize("HeadQuarters");
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
            HPSlider.value = stat.Status.hp / stat.Status.maxHp;
    }

    /// <summary>
    /// 본부에게 데미지를 입히는 메서드입니다. 체력이 0 이하면 FinishGame을 호출합니다.
    /// </summary>
    public override void TakeDamage(float damage, GameObject attacker = null)
    {
        stat.Status.hp -= damage;

        if (stat.Status.hp <= 0 && !broken)
        {
            broken = true;
            var go = GameObject.Find("@Scene").GetComponent<GameScene>();
            go.FinishGame(LayerMask.LayerToName(gameObject.layer));
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(HPSlider.value);
        }
        else
        {
            HPSlider.value = (float)stream.ReceiveNext();
        }
    }
}
