using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ChampionManager : Controller
{
    public int skillPoint;

    HeroCombat heroCombatScript;
    PlayerAnimation playerAnim;
    ChampionStat stat;

    [SerializeField] GameObject currentAttacker;

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

    public override void TakeDamage(float damage, GameObject attacker = null)
    {
        if (attacker != null)
        {
            currentAttacker = attacker;
        }

        if (stat == null)
            Debug.Log("Error Stat");
        else
            stat.Status.hp -= stat.Status.atk * (100 / (100 + stat.Status.defense));

        if (stat.Status.hp <= 0)
        {
            Invoke("OnDie", 30f);
            return;
        }
    }

    public void OnDie()
    {
        if(Util.GetLocalPlayerId() <= 5)
        {
            // 블루팀 리스폰
            transform.Translate(new Vector3(-105, 0, -105));
        }
        else
        {
            // 레드팀 리스폰
            transform.Translate(new Vector3(105, 0, 105));
        }
    }
}
