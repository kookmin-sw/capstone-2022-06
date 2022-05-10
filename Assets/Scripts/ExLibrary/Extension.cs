using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    /*
    GetComponent의 결과가 null이면 AddComponent를 호출하는 편의성 메서드
    */
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        T ret = go.GetComponent<T>();
        if (ret is not null)
        {
            return ret;
        }
        return go.AddComponent<T>();
    }

    /// <summary>
    /// GameObject go에게 UI_EventHandler와 action을 부착함
    /// eventType의 기본값은 Click
    /// </summary>
    public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent eventType = Define.UIEvent.Click)
    {
        UI_EventHandler evt = go.GetOrAddComponent<UI_EventHandler>();

        switch (eventType)
        {
            case Define.UIEvent.Click:
            {
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            }
            case Define.UIEvent.Drag:
            {
                evt.OnDragHandler -= action;
                evt.OnClickHandler += action;
                break;
            }
        }
    }

    /// <summary>
    /// GameObject go에게 action을 제거함.
    /// 제거 과정 중 UI_EventHandler가 없는 경우 부착함.
    /// </summary>
    public static void DisableEvent(this GameObject go, Action<PointerEventData> action)
    {
        UI_EventHandler evt = go.GetOrAddComponent<UI_EventHandler>();

        evt.OnClickHandler -= action;
        evt.OnDragHandler -= action;
    }
}
