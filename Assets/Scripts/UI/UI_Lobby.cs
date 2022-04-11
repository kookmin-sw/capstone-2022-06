using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lobby : UI_Scene
{
    private GameObject authPanel, matchmakingPanel;
    
    enum GameObjects
    {
        MatchmakingPanel,
        AuthPanel,
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        matchmakingPanel = Get<GameObject>((int)GameObjects.MatchmakingPanel);
        authPanel = Get<GameObject>((int)GameObjects.AuthPanel);
        authPanel.SetActive(true);
        matchmakingPanel.SetActive(false);
    }
}
