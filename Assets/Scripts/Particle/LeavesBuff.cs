using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeavesBuff : ParticleBase
{
    void Start()
    {
        base.Init();
        Invoke("GiveHeal", 1.5f);
    }

    /// <summary>
    /// 범위 내 아군에게 힐을 주는 코드
    /// </summary>
    void GiveHeal()
    {
        Collider[] friends = ScanFriendlies(7f);

        for (int i = 0; i < friends.Length; i++)
        {
            ObjectStat stat = friends[i].gameObject.GetComponent<ObjectStat>();
            PhotonView pv = stat.gameObject.GetPhotonView();

            if (!stat || !pv)
            {
                continue;
            }

            stat.UpdateGoHP(pv.ViewID, 120f);
        }
    }
}
