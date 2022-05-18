using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class SkillPaths
{
    public string passive, q, w, e, r;
}

public class UI_ChampSkill : UI_Base
{
    TextAsset skillIcons;

    enum GameObjects
    {
        Passive,
        Skill_Q,
        Skill_W,
        Skill_E,
        Skill_R
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        string mySkillPath = (string)Util.GetLocalPlayerProp("skillPath");
        skillIcons = Managers.Resource.Load<TextAsset>(mySkillPath);
    }
}
