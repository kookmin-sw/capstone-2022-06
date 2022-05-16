using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ComSkill : UI_Base
{
    UI_AreaIndicator skillPointer;
    int skillMask = 0;
    int skillType;
    int skillAble = 0;
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

        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (!Get<GameObject>((int)GameObjects.Meteor).GetComponent<UI_Cooldown>().IsCooldownFinished())
            {
                return;
            }

            skillAble ^= 1;
            skillType = 0;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            if (!Get<GameObject>((int)GameObjects.Heal).GetComponent<UI_Cooldown>().IsCooldownFinished())
            {
                return;
            }

            skillAble ^= 1;
            skillType = 1;
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            skillAble = 0;
        }

        // skillAble에 따라 skillPointer 활성화 혹은 비활성화
        skillPointer.gameObject.SetActive(skillAble == 1);
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
