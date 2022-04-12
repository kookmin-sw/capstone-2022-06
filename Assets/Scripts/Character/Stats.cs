using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHealth;
    public float healthRegen;
    public float health;
    public float armor;
    public float attackDmg;
    public float abilityPower;
    public float mana;
    public float manaRegen;
    public float speed;
    public float attackSpeed;
    public float attackTime;

    HeroCombat heroCombatScript;
    PlayerAnimation playerAnim;

    // 플레이어의 스탯 스크립트

    void Start()
    {
        heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();
        playerAnim = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        playerAnim.agent.speed = speed;

        if (health <= 0)
        {
            Destroy(gameObject);
            heroCombatScript.targetedEnemy = null;
            heroCombatScript.performMeleeAttack = false;
        }
    }
}
