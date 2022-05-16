using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ComSkill : UI_Base
{
    UI_AreaIndicator skillPointer;
    int skillType;
    string[] skillPaths = {
        "Arts/Particles/Meteor",
        "Arts/Particles/LeavesBuff"
    };

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    void Update()
    {
        OnKeyPressed();
    }

    /// <summary> 지휘관 스킬 키를 눌렀을 때 일어나는 동작을 작성한 메서드 </summary>
    void OnKeyPressed()
    {
        skillType = -1;

        if (Input.GetKey(KeyCode.Q))
        {
            if (!Get<UI_Cooldown>((int)GameObjects.Meteor).IsCooldownFinished())
            {
                return;
            }

            skillType = 0;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            if (!Get<UI_Cooldown>((int)GameObjects.Heal).IsCooldownFinished())
            {
                return;
            }

            skillType = 1;
        }

        // 스킬 사용가능, skillPointer 활성화
        if (skillType > -1)
        {
            skillPointer = Managers.UI.AttachWorldUI<UI_AreaIndicator>();
        }
    }

    enum GameObjects
    {
        Meteor,
        Heal
    }
}
