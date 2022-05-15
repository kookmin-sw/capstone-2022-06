using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ComSkill : UI_Base
{
    enum GameObjects
    {
        Meteor,
        Heal
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }
}
