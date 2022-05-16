using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Meteor : ParticleBase
{
    void Start()
    {
        base.Init();

        // 실험을 위한 코드
        Physics.OverlapSphere(transform.position, 7f, LayerMask.GetMask("BlueTeam"));
    }
}
