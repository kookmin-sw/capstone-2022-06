using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ComSkill : UI_Base
{
    UI_AreaIndicator skillPointer;
    int skillMask = 0;
    int skillType;
    bool skillAble = false;
    string[] skillPaths = {
        "Arts/Particles/Meteor",
        "Arts/Particles/LeavesBuff"
    };
    UnityEngine.KeyCode[] keyIndex = {
        KeyCode.Q,
        KeyCode.W
    };

    Dictionary<UnityEngine.KeyCode, int> keyIndexDict = new Dictionary<KeyCode, int>();

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        skillPointer = Managers.UI.AttachWorldUI<UI_AreaIndicator>();
        skillPointer.gameObject.SetActive(false);

        for (int i = 0; i < keyIndex.Length; i++)
        {
            keyIndexDict[keyIndex[i]] = i + 1;
        }
    }

    void Update()
    {
        OnKeyPressed();
    }

    /// <summary> 지휘관 스킬 키를 눌렀을 때 일어나는 동작을 작성한 메서드 </summary>
    void OnKeyPressed()
    {
        skillType = -1;

        // escape 감지시 스킬 비활성화는 게임 강제 종료 이슈를 고려하여 보류
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
            if (skillMask > 0)
            {
                skillAble = true;
                skillMask = 1 << 0;
            }
            else
            {
                skillAble = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            if (!Get<GameObject>((int)GameObjects.Heal).GetComponent<UI_Cooldown>().IsCooldownFinished())
            {
                return;
            }

            skillMask ^= (1 << 1);
            if (skillMask > 0)
            {
                skillAble = true;
                skillMask = 1 << 1;
            }
            else
            {
                skillAble = false;
            }
        }

        // skillAble에 따라 skillPointer 활성화 혹은 비활성화
        skillPointer.gameObject.SetActive(skillAble);
    }

    void PropagateSkillState(bool able)
    {

    }

    enum GameObjects
    {
        Meteor,
        Heal
    }
}
