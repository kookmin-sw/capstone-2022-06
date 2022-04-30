using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int level;
    public int exp;
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
    Ability ability;

    // 플레이어의 스탯 스크립트

    void Start()
    {
        heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();
        playerAnim = GetComponent<PlayerAnimation>();
        ability = GetComponent<Ability>();
    }

    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            heroCombatScript.targetedEnemy = null;
            heroCombatScript.performMeleeAttack = false;
        }

        playerAnim.agent.speed = speed;
    }

    void FixedUpdate()
    {
        healthRegen = 2 + level * 1.5f;

        health += healthRegen;
    }
}
