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
        string mySkillPath = (string)Util.GetLocalPlayerProp("skills");
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

    /// <summary>
    /// 스킬 아이콘이 담긴 go에서 스프라이트를 path에 있는 이미지로 바꾸는 메서드
    /// 스킬 포인트가 바뀌는 것을 방지하기 위해 Point 스트링이 감지된 경우 무시합니다.
    /// </summary>
    private void UpdateSkillIcon(GameObject go, string path)
    {
        Sprite loadedSprite = Managers.Resource.Load<Sprite>(path);

        foreach (Transform item in go.transform)
        {
            if (item.name.IndexOf("Point") != -1)
            {
                continue;
            }

            Image itemImage = item.gameObject.GetComponent<Image>();

            if (itemImage)
            {
                itemImage.sprite = loadedSprite;
            }
        }
    }
}
