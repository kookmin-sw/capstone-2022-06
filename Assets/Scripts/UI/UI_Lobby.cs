using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lobby : UI_Base
{
    enum GameObjects
    {
        MatchMakingPanel,
        AuthPanel,
    }

    private GameObject authPanel, matchmakingPanel;

    void Awake()
    {
        // Get<GameObject>()
        Init();
        authPanel.SetActive(true);
        matchmakingPanel.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
        Bind<GameObject>(typeof(GameObjects));
        matchmakingPanel = Get<GameObject>((int)GameObjects.MatchMakingPanel);
        authPanel = Get<GameObject>((int)GameObjects.AuthPanel);
    }
}
