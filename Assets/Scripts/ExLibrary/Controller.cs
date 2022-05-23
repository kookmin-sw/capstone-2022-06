using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public abstract class Controller : MonoBehaviourPunCallbacks
{
    public bool isShooting;

    public abstract void TakeDamage(float damage, GameObject attacker = null);
}
