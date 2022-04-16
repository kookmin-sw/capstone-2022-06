using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxHealth;
    public float health;

    HeroCombat heroCombatScript;

    Rigidbody rigid;
    CapsuleCollider capCollider;

    // 에너미의 스탯 스크립트

    void Start()
    {
        heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();
        rigid = GetComponent<Rigidbody>();
        capCollider = GetComponent<CapsuleCollider>();
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

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "THSSkill_E")
        {
            THSSkill_EProj tHSSkill_Eproj = other.GetComponent<THSSkill_EProj>();
            Debug.Log("hit E");
            health -= tHSSkill_Eproj.damage;
            Destroy(other.gameObject);
        }

        if (other.tag == "THSSkill_R")
        {
            THSSkill_RProj tHSSkill_Rproj = other.GetComponent<THSSkill_RProj>();
            Debug.Log("hit R");
            health -= tHSSkill_Rproj.damage;
            Destroy(other.gameObject);
        }
    }
}