using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InputTargeting : MonoBehaviour
{
    // 카메라에서 플레이어가 마우스로 타겟팅 하는 물체를 찾기 위한 스크립트

    public GameObject selectedHero;
    public bool heroPlayer;
    RaycastHit hit;
    public int _mask;

    GameObject _target;

    void Awake()
    {
        _mask = 1 << Util.GetEnemyLayer();
    }

    void Start()
    {
        Invoke("SetChamp", 1f);
    }

    void LateUpdate()
    {
        // 챔피언 타겟팅
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _mask))
            {
                // 타겟팅이 가능한 경우
                if (hit.collider.GetComponent<Targetable>() != null && selectedHero.GetPhotonView().ViewID != hit.collider.gameObject.GetPhotonView().ViewID)
                {
                    selectedHero.GetComponent<HeroCombat>().SetTargetedEnemy(hit.collider.gameObject.GetPhotonView().ViewID);
                }
                else
                {
                    selectedHero.GetComponent<HeroCombat>().SetTargetedEnemyNull();
                }
            }
            else
            {
                selectedHero.GetComponent<HeroCombat>().SetTargetedEnemyNull();
            }
        }

    }

    void SetChamp()
    {
        GameObject[] ar = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in ar)
        {
            if (go.GetPhotonView().IsMine)
            {
                selectedHero = go;
                return;
            }
        }
    }

    // public IEnumerator Initialize(GameObject player)
    // {
    //     selectedHero = player;
        
    //     yield return new WaitForSeconds(1.0f);


    //     if (selectedHero.layer == LayerMask.NameToLayer("RedTeam"))
    //         _mask = LayerMask.GetMask("BlueTeam");
    //     else
    //         _mask = LayerMask.GetMask("RedTeam");
    // }

    // public void StartInitialize()
    // {
    //     StartCoroutine("Initialize");
    // }
}
