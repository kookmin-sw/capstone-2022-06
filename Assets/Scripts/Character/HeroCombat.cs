using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCombat : MonoBehaviour
{
    public enum HeroAttackType { Melee, Ranged };
    public HeroAttackType heroAttackType;

    public GameObject targetedEnemy;
    public float attackRange;
    public float rotateSpeedForAttack;

    private ClickMovement moveScript;

    public bool basicAtkIdle = false;
    public bool isHeroAlive;
    public bool performMeleeAttack = true;

    void Start()
    {
        moveScript = GetComponent<ClickMovement>();
    }

    void Update()
    {
        if(targetedEnemy != null)
        {
            if(Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > attackRange)
            {
                moveScript.agent.SetDestination(targetedEnemy.transform.position);
                moveScript.agent.stoppingDistance = attackRange;

                // 방향
                Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref moveScript.rotateVelocity, rotateSpeedForAttack * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
            else
            {
                if(heroAttackType == HeroAttackType.Melee)
                {
                    if(performMeleeAttack)
                    {
                        Debug.Log("Attack Minion");
                        
                        // 미니언 공격 코루틴

                    }
                }
            }
        }
    }
}
