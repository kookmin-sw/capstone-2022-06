using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TempleController : Controller, IPunObservable
{
    HQStat stat;

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

    public override void TakeDamage(float damage, GameObject attacker = null)
    {
        stat.Status.hp -= damage;

        if (stat.Status.hp <= 0)
        {
            // ³Ø¼­½º ÆÄ±«
            // ½Â¸®ÆÀ ¼³Á¤ ¹× °ÔÀÓ Á¾·á
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
