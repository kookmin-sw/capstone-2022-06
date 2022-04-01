using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    // 캔버스 정렬 우선순위
    int _order = 10;

    /*
    게임 오브젝트에 캔버스를 부착함
    */
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
}
