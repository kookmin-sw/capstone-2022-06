using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float attackDmg;
    public float attackSpeed;
    public float attackTime;

    HeroCombat heroCombatScript;

    // 에너미의 스탯 스크립트

    void Start()
    {
        heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            heroCombatScript.targetedEnemy = null;
            heroCombatScript.performMeleeAttack = false;
        }
    }
}