using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionStat : ObjectStat
{
    public override void Initialize(string name)
    {
        stat.Init(Managers.Data.StatDict[name]);
    }
}
