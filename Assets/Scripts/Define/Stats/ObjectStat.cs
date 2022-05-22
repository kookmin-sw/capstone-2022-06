using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class ObjectStat : MonoBehaviour
{
    [SerializeField] protected Stat stat;

    public Stat Status { get { return stat; } }

    public abstract void Initialize(string name);

    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    /// <summary>
    /// RPC를 호출하는 대리 메서드
    /// </summary>
    public void UpdateGoHP(float diff)
    {
        if (PV.IsMine)
        {
            PV.RPC("UpdateGoHPRPC", RpcTarget.All, diff);
        }
    }

    /// <summary>
    /// 인자로 받은 게임 오브젝트의 HP를 갱신하는 RPC 메서드
    /// </summary>
    [PunRPC]
    public void UpdateGoHPRPC(float diff)
    {
        stat.hp += diff;
        stat.hp = Mathf.Max(0, stat.hp);
        stat.hp = Mathf.Min(stat.maxHp, stat.hp);
    }
}
