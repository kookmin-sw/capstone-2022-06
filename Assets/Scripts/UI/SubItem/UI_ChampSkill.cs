using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
class SkillPaths
{
    public string passive, q, w, e, r, d, f;
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
        Skill_R,
        Ability_D,
        Ability_F
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        string mySkillPath = (string)Util.GetLocalPlayerProp("skillPath");
        skillIcons = Managers.Resource.Load<TextAsset>(mySkillPath);

        SkillPaths myIcons = JsonUtility.FromJson<SkillPaths>(skillIcons.text);

        UpdateSkillIcon(Get<GameObject>((int)GameObjects.Passive), myIcons.passive);
        UpdateSkillIcon(Get<GameObject>((int)GameObjects.Skill_Q), myIcons.q);
        UpdateSkillIcon(Get<GameObject>((int)GameObjects.Skill_W), myIcons.w);
        UpdateSkillIcon(Get<GameObject>((int)GameObjects.Skill_E), myIcons.e);
        UpdateSkillIcon(Get<GameObject>((int)GameObjects.Skill_R), myIcons.r);
        UpdateSkillIcon(Get<GameObject>((int)GameObjects.Ability_D), myIcons.d);
        UpdateSkillIcon(Get<GameObject>((int)GameObjects.Ability_F), myIcons.f);
    }

    private void UpdateSkillIcon(GameObject go, string path)
    {
        Sprite loadedSprite = Managers.Resource.Load<Sprite>(path);

        foreach (Transform item in go.transform)
        {
            Image itemImage = item.gameObject.GetComponent<Image>();

            if (itemImage)
            {
                itemImage.sprite = loadedSprite;
            }
        }
    }
}
