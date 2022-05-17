using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ParticleBase : MonoBehaviour
{
    void Start()
    {
        Init();
    }

    public virtual void Init()
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

    /// <summary> 상대팀 유닛들을 overlapSphere로 스캔하는 메서드 </summary>
    protected Collider[] ScanOppositeness(float radius)
    {
        int enemyLayer = Util.GetEnemyLayer();
        return Physics.OverlapSphere(transform.position, radius, 1 << enemyLayer);
    }

    /// <summary> 아군 유닛들을 overlapSphere로 스캔하는 메서드 </summary>
    protected Collider[] ScanFriendlies(float radius)
    {
        int friendLayer = Util.GetEnemyLayer();
        return Physics.OverlapSphere(transform.position, radius, 1 << friendLayer);
    }
}
