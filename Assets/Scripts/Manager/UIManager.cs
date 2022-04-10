using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    // 캔버스 정렬 우선순위
    int _order = 10;

    UI_Scene _sceneUI = null;

    /// <summary>
    /// 전체 UI에 상주하는 UI 루트 오브젝트
    /// </summary>
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");

            if (!root)
            {
                root = new GameObject();
                root.name = "@UI_Root";
            }

            return root;
        }
    }

    /// <summary>
    /// 인자로 주어진 게임 오브젝트에 캔버스를 부착합니다.
    /// </summary>
    public void SetCanvas(GameObject go, bool ordering = true)
    {
        if (go is null)
        {
            return;
        }

        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        canvas.sortingOrder = (ordering ? _order++ : 0);
    }

    /*
    월드 공간에 존재하는 오브젝트에 UI를 부착함
    */
    public T AttachWorldUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject ui = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");
        ui.layer = LayerMask.NameToLayer("UI");

        if (ui is null)
        {
            return null;
        }

        if (parent is not null)
        {
            ui.transform.SetParent(parent);
        }
        
        Canvas canvas = ui.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return ui.GetOrAddComponent<T>();
    }

    /*
    미니맵에 보여질 UI 마커를 전용 카메라에 보이도록 부착함
    */
    public T AttachMapMarker<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject ui = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");
        ui.layer = LayerMask.NameToLayer("MapUI");

        if (ui is null)
        {
            return null;
        }

        if (parent is not null)
        {
            ui.transform.SetParent(parent);
        }

        Canvas canvas = ui.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        return ui.GetOrAddComponent<T>();
    }

    /// <summary>
    /// 매개변수로 받은 이름에 해당하는 UI 프리팹을 게임 씬에 출력할 수 있도록 리턴합니다.
    /// </summary>
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        if (!go)
        {
            Debug.Log($"Failed to find UI: {name}");
            return null;
        }

        T sceneUI = go.GetOrAddComponent<T>();
        _sceneUI = sceneUI;
        go.transform.SetParent(Root.transform);
        return sceneUI;
    }

    /// <summary>
    /// 화면에 보이는 모든 UI를 닫습니다. (인게임 UI 제외)
    /// </summary>
    public void Clear()
    {
        _sceneUI = null;
    }
}
