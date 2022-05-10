using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTargeting : MonoBehaviour
{
    // 카메라에서 플레이어가 마우스로 타겟팅 하는 물체를 찾기 위한 스크립트

    public GameObject selectedHero;
    public bool heroPlayer;
    RaycastHit hit;

    void Start()
    {
        selectedHero = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // 미니언 타겟팅
        if(Input.GetMouseButtonDown(1))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                // 타겟팅이 가능한 경우
                if(hit.collider.GetComponent<Targetable>() != null)
                {
                    if(hit.collider.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Minion)
                    {
                        selectedHero.GetComponent<HeroCombat>().targetedEnemy = hit.collider.gameObject;
                    }
                    else if (hit.collider.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Champion)
                    {
                        selectedHero.GetComponent<HeroCombat>().targetedEnemy = hit.collider.gameObject;
                    }
                }
                else if(hit.collider.gameObject.GetComponent<Targetable>() == null)
                {
                    selectedHero.GetComponent<HeroCombat>().targetedEnemy = null;
                }
            }
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
}
