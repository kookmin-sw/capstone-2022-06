using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExSceneManager
{
    public BaseScene currentScene
    {
        get { return GameObject.FindObjectOfType<BaseScene>(); }
    }

    public void LoadScene(Define.Scene sceneType)
    {
        Managers.Clear();
        ClearScene();
        SceneManager.LoadScene(getSceneName(sceneType));
    }

    private string getSceneName(Define.Scene sceneType)
    {
        return sceneType.ToString();
    }

    public void ClearScene()
    {
        currentScene.Clear();
    }
}
