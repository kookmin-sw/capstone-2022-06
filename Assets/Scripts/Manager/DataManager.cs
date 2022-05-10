using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager 
{
    public Dictionary<string, Stat> StatDict { get; private set; } = new Dictionary<string, Stat>();    // √®««æ Ω∫≈»¿ª ¿˙¿Â«— µÒº≈≥ ∏Æ
    public Dictionary<string, Stat> MinionStatDict { get; private set; } = new Dictionary<string, Stat>();
    public Dictionary<string, Stat> TurretStatDict { get; private set; } = new Dictionary<string, Stat>();
    public Dictionary<string, Stat> MonsterStatDict { get; private set; } = new Dictionary<string, Stat>();

    public void Init()
    {
        StatDict = LoadJson<StatData, string, Stat>("StatData").MakeDict();
        MinionStatDict = LoadJson<StatData, string, Stat>("MinionStatData").MakeDict();
        TurretStatDict = LoadJson<StatData, string, Stat>("TurretStatData").MakeDict();
        MonsterStatDict = LoadJson<StatData, string, Stat>("MonsterStatData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
