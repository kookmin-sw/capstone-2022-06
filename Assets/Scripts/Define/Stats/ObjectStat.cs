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
    /// 인자로 받은 게임 오브젝트의 HP를 갱신하는 RPC 메서드
    /// </summary>
    [PunRPC]
    public void UpdateGoHp(int id, float diff)
    {
        GameObject go = PhotonView.Find(id).gameObject;
        ObjectStat _stat = go.GetComponent<ObjectStat>();

        if (!_stat)
        {
            return;
        }

        _stat.stat.hp += diff;
        _stat.stat.hp = Mathf.Max(0, _stat.stat.hp);
        _stat.stat.hp = Mathf.Min(_stat.stat.maxHp, _stat.stat.hp);
    }
}
