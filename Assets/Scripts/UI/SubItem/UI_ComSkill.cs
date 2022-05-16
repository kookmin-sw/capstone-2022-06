using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ComSkill : UI_Base
{
    UI_AreaIndicator skillPointer;
    int skillMask = 0;
    bool skillAble = false;
    string[] skillPaths = {
        "Arts/Particles/Meteor",
        "Arts/Particles/LeavesBuff"
    };

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        skillPointer = Managers.UI.AttachWorldUI<UI_AreaIndicator>();
        skillPointer.gameObject.SetActive(false);
    }

    void Update()
    {
        OnKeyPressed();
    }

    /// <summary> 지휘관 스킬 키를 눌렀을 때 일어나는 동작을 작성한 메서드 </summary>
    void OnKeyPressed()
    {
        // escape 감지시 스킬 비활성화는 게임 종료 이슈를 고려하여 보류
        // if (Input.GetKeyUp(KeyCode.Escape))
        // {
        //     skillPointer.gameObject.SetActive(skillAble);
        //     return;
        // }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (!Get<GameObject>((int)GameObjects.Meteor).GetComponent<UI_Cooldown>().IsCooldownFinished())
            {
                return;
            }

            skillMask ^= (1 << 0);
            skillAble = skillMask > 0;
            skillMask = skillAble ? 1 << 0 : 0;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            if (!Get<GameObject>((int)GameObjects.Heal).GetComponent<UI_Cooldown>().IsCooldownFinished())
            {
                return;
            }

            skillMask ^= (1 << 1);
            skillAble = skillMask > 0;
            skillMask = skillAble ? 1 << 1 : 0;
        }

        // skillAble에 따라 skillPointer 활성화 혹은 비활성화
        skillPointer.gameObject.SetActive(skillAble);
    }

    /// <summary>
    /// 현재 활성화된 스킬을 찾습니다. 없으면 null을 리턴합니다.
    /// </summary>
    string GetCurrentSkillPath()
    {
        for (int i = 0; i < skillPaths.Length; i++)
        {
            if ((skillMask & (1 << i)) > 0)
            {
                return skillPaths[i];
            }
        }

        return null;
    }

    enum GameObjects
    {
        Meteor,
        Heal
    }
}
