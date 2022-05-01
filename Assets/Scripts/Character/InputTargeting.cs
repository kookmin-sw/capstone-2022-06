using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTargeting : MonoBehaviour
{
    // ī�޶󿡼� �÷��̾ ���콺�� Ÿ���� �ϴ� ��ü�� ã�� ���� ��ũ��Ʈ

    public GameObject selectedHero;
    public bool heroPlayer;
    RaycastHit hit;

    void Start()
    {
        selectedHero = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // �̴Ͼ� Ÿ����
        if(Input.GetMouseButtonDown(1))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                // Ÿ������ ������ ���
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
        // è�Ǿ� Ÿ����
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                // Ÿ������ ������ ���
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
