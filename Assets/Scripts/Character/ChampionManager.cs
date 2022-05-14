using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionManager : MonoBehaviour
{
    /*
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
    */

    public int skillPoint;

    HeroCombat heroCombatScript;
    PlayerAnimation playerAnim;
    ChampionStat stat;

    void Start()
    {
        heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();
        playerAnim = GetComponent<PlayerAnimation>();
        stat = GetComponent<ChampionStat>();
        stat.Initialize("Mangoawl");
    }

    void Update()
    {
        if(stat.Status.level < 10)
        {
            LevelUp();
        }

        if (stat.Status.hp > stat.Status.maxHp)
        {
            stat.Status.hp = stat.Status.maxHp;
        }

        if (stat.Status.hp <= 0)
        {
            Destroy(gameObject);
            heroCombatScript.targetedEnemy = null;
            heroCombatScript.performMeleeAttack = false;
        }

        playerAnim.agent.speed = stat.Status.moveSpeed;
    }

    void FixedUpdate()
    {
        stat.Status.healthRegen = 2 + stat.Status.level * 1.5f;

        stat.Status.hp += stat.Status.healthRegen;
    }

    void LevelUp()
    {
        if(stat.Status.exp >= 100)
        {
            stat.Status.exp = 0;
            stat.Status.level++;
            skillPoint++;
        }
    }
}
