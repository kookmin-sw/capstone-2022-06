using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTargeting : MonoBehaviour
{
    // 카메라에서 플레이어가 마우스로 타겟팅 하는 물체를 찾기 위한 스크립트

    public GameObject selectedHero;
    public bool heroPlayer;
    RaycastHit[] hits;
    public int _mask;

    GameObject _target;

    void Start()
    {
       
    }

    void LateUpdate()
    {
        // 미니언 타겟팅
        if(Input.GetMouseButtonDown(1))
        {
            hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, _mask);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.GetComponent<CapsuleCollider>() != null)
                {
                    if (hit.collider == hit.collider.gameObject.GetComponent<CapsuleCollider>())
                    {
                        _target = hit.collider.gameObject;
                        break;
                    }
                    else
                        _target = null;
                }
                else
                    _target = null;
            }

            if (_target)
                selectedHero.GetComponent<HeroCombat>().targetedEnemy = _target;
            else
                selectedHero.GetComponent<HeroCombat>().targetedEnemy = null;
        }

        /*
        // 챔피언 타겟팅
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                // 타겟팅이 가능한 경우
                if (hit.collider.GetComponent<Targetable>() != null)
                {
                    if (hit.collider.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Champion)
                    {
                        selectedHero.GetComponent<HeroCombat>().targetedEnemy = hit.collider.gameObject;
                    }
                }
                else if (hit.collider.gameObject.GetComponent<Targetable>() == null)
                {
                    selectedHero.GetComponent<HeroCombat>().targetedEnemy = null;
                }
            }
        }
        */
    }

    public void Initialize(GameObject player)
    {
        selectedHero = player;

        if (selectedHero.layer == LayerMask.NameToLayer("RedTeam"))
            _mask = LayerMask.GetMask("BlueTeam");
        else
            _mask = LayerMask.GetMask("RedTeam");
    }
}
