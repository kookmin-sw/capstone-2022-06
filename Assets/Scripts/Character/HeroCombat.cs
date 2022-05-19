using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 플레이어의 전투 관련 스크립트
 */

public class HeroCombat : MonoBehaviour
{
    public enum HeroAttackType { Melee, Ranged };
    public HeroAttackType heroAttackType;

    public GameObject targetedEnemy;
    public float attackRange;
    public float rotateSpeedForAttack;

    private ClickMovement moveScript;
    private ChampionStat stat;
    public Animator anim;
    public Ability ability;

    public bool basicAtkIdle = false;
    public bool isHeroAlive;
    public bool performMeleeAttack = true;

    public float distance;

    void Start()
    {
        moveScript = GetComponent<ClickMovement>();
        stat = GetComponent<ChampionStat>();
        stat.Initialize("Mangoawl");
        anim = GetComponentInChildren<Animator>();
        ability = GetComponent<Ability>();
    }

    void Update()
    {
        if(targetedEnemy != null)
        {
            distance = Vector3.Distance(transform.position, targetedEnemy.transform.position);
            if(Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > attackRange)
            {
                moveScript.agent.isStopped = false;
                moveScript.agent.SetDestination(targetedEnemy.transform.position);
                moveScript.agent.stoppingDistance = attackRange;

                // 방향
                Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref moveScript.rotateVelocity, rotateSpeedForAttack * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
            else
            {
                if(targetedEnemy != null)
                {
                    moveScript.agent.isStopped = true;
                    if (heroAttackType == HeroAttackType.Melee)
                    {
                        if (performMeleeAttack)
                        {
                            // 공격 코루틴
                            StartCoroutine(MeleeAttackInterval());
                        }
                    }
                }
                
            }
        }
    }

    IEnumerator MeleeAttackInterval()
    {
        performMeleeAttack = false;
        anim.SetBool("BasicAttack", true);
        
        yield return new WaitForSeconds(stat.Status.atkInterTime / ((100 + stat.Status.atkInterTime) * 0.01f));

        if (targetedEnemy == null || ability.isSkill_E == true)
        {
            anim.SetBool("BasicAttack", false);
            performMeleeAttack = true;
        }
        
    }

    public void MeleeAttack()
    {
        /*
        if(targetedEnemy != null)
        {
            if(targetedEnemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Minion)
            {
                Debug.Log("attack Minion");
                targetedEnemy.GetComponent<EnemyStats>().health -= stat.Status.atk * (100 / (100 + targetedEnemy.GetComponent<EnemyStats>().armor));
            }

            if(targetedEnemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Champion)
            {
                Debug.Log("attack Champion");
                targetedEnemy.GetComponent<ChampionStat>().Status.hp -= stat.Status.atk * (100 / (100 + targetedEnemy.GetComponent<ChampionStat>().Status.defense));
            }
        }
        */
        targetedEnemy.GetComponent<Controller>().TakeDamage(stat.Status.atk, this.gameObject);

        performMeleeAttack = true;
    }

}
