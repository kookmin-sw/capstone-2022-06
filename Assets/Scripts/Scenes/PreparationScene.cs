using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparationScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        sceneType = Define.Scene.Lobby;

        Managers.UI.ShowSceneUI<UI_Preparation>();
    }

    public override void Clear() {}
}
