using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public abstract class Controller : MonoBehaviourPunCallbacks
{
    public abstract void TakeDamage(float damage);
}
