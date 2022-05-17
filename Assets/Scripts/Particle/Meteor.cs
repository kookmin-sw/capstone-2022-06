using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Meteor : ParticleBase
{
    void Start()
    {
        base.Init();
        if (PhotonNetwork.IsMasterClient)
        {
            Invoke("GiveDamage", 0.5f);
        }
    }

    /// <summary>
    /// 반경 내 적에게 데미지를 입히는 함수입니다.
    /// 아직 미니언에게만 데미지를 입힙니다.
    /// </summary>
    void GiveDamage()
    {
        Collider[] candidates = ScanOppositeness(6f);

        for (int i = 0; i < candidates.Length; i++)
        {
            RaycastHit hit;
            Vector3 dir = (candidates[i].transform.position - transform.position);
            float dist = dir.magnitude;

            // hit 한 경우 무시
            if (Physics.Raycast(transform.position, dir.normalized, out hit, dist, LayerMask.GetMask("Obstacle")))
            {
                continue;
            }

            GameObject target = candidates[i].gameObject;
            Controller targetStat = target.GetComponent<Controller>();

            if (!targetStat)
            {
                continue;
            }

            targetStat.TakeDamage(115f);
        }
    }
}
