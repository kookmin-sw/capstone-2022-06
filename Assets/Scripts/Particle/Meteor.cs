using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Meteor : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitToFinish());
    }

    IEnumerator WaitToFinish()
    {
        yield return new WaitUntil(() => {
            return GetComponent<ParticleSystem>().isPlaying is false;
        });
        PhotonNetwork.Destroy(gameObject);
    }
}
