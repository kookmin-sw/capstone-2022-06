using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class UI_Base : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _uiobjects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    private void Start()
    {
        Init();
    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = type.GetEnumNames();
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.SearchChild(gameObject, names[i], true);
            }
            else
            {
                objects[i] = Util.SearchChild<T>(gameObject, names[i], true);
            }
        
            if (!objects[i])
            {
                Debug.Log($"Failed to bind {names[i]}");
            }
        }

        _uiobjects.Add(typeof(T), objects);
    }

    /// <summary>
    /// 인자로 받은 인덱스에 해당하는 UI 컴포넌트 혹은 오브젝트를 받습니다.
    /// 인덱스의 의미는 상속받은 클래스에 정의된 자식 컴포넌트의 이름을 나열한 enum의 순서가 됩니다.
    /// </summary>
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects;

        if (!_uiobjects.TryGetValue(typeof(T), out objects))
        {
            Debug.Log($"Failed to load {typeof(T).Name}");
            return null;
        }

        return objects[idx] as T;
    }

    /// <summary>
    /// 버튼 컴포넌트를 가져오는 Get의 래핑 메서드입니다.
    /// </summary>
    protected Button GetButton(int idx)
    {
        return Get<Button>(idx);
    }

    /// <summary>
    /// 텍스트 컴포넌트를 가져오는 Get의 래핑 메서드입니다.
    /// </summary>
    protected Text GetText(int idx)
    {
        return Get<Text>(idx);
    }

    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent eventType = Define.UIEvent.Click)
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
}
