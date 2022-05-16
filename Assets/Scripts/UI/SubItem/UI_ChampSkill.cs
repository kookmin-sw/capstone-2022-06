using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ChampSkill : UI_Base
{
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    void Update()
    {
        OnKeyPressed();
    }

    void OnKeyPressed()
    {
        if (Input.GetKey("q"))
        {

        }
        else if (Input.GetKey("w"))
        {

        }
    }

    enum GameObjects
    {
        Meteor,
        Heal
    }
}
