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
        int mask;
        if (gameObject.layer == LayerMask.NameToLayer("BlueTeam"))
        {
            mask = LayerMask.GetMask("RedTeam");
        }
        else
        {
            mask = LayerMask.GetMask("BlueTeam");
        }
        return Physics.OverlapSphere(transform.position, radius, mask);
    }

    /// <summary> 아군 유닛들을 overlapSphere로 스캔하는 메서드 </summary>
    protected Collider[] ScanFriendlies(float radius)
    {
        return Physics.OverlapSphere(transform.position, radius, 1 << gameObject.layer);
    }
}
