using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Stat
[Serializable]
public class Stat
{
    [SerializeField] public string name;
    [SerializeField] public float level;
    [SerializeField] public float exp;
    [SerializeField] public float hp;
    [SerializeField] public float mp;
    [SerializeField] public float maxHp;
    [SerializeField] public float maxMp;
    [SerializeField] public float atk;
    [SerializeField] public float ap;
    [SerializeField] public float healthRegen;
    [SerializeField] public float manaRegen;
    [SerializeField] public float defense;
    [SerializeField] public float atkSpeed;
    [SerializeField] public int GivingGold;
    [SerializeField] public float GivingExp;
    [SerializeField] public float moveSpeed;
    [SerializeField] public int priority;
    [SerializeField] public float atkInterTime;

    public void Init(Stat loadStat)
    {
        this.name = loadStat.name;
        this.level = loadStat.level;
        this.exp = loadStat.exp;
        this.hp = loadStat.hp;
        this.mp = loadStat.mp;
        this.maxHp = loadStat.maxHp;
        this.maxMp = loadStat.maxMp;
        this.atk = loadStat.atk;
        this.ap = loadStat.ap;
        this.healthRegen = loadStat.healthRegen;
        this.manaRegen = loadStat.manaRegen;
        this.defense = loadStat.defense;
        this.atkSpeed = loadStat.atkSpeed;
        this.GivingGold = loadStat.GivingGold;
        this.GivingExp = loadStat.GivingExp;
        this.moveSpeed = loadStat.moveSpeed;
        this.priority = loadStat.priority;
        this.atkInterTime = loadStat.atkInterTime;
    }
}

[Serializable]
public class StatData : ILoader<string, Stat>
{
    public List<Stat> stats = new List<Stat>();

    public Dictionary<string, Stat> MakeDict()
    {
        Dictionary<string, Stat> dict = new Dictionary<string, Stat>();

        foreach (Stat stat in stats)
            dict.Add(stat.name, stat);
        return dict;
    }
}
#endregion
