using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    /*
    go의 자식 오브젝트 중 제네릭 T에 해당하는 컴포넌트가 있는지 찾고 없으면 null을 리턴한다.
    name == null일 경우 오브젝트의 이름은 상관 없이 컴포넌트 타입이 T이기만 하면 먼저 찾은 T를 반환한다.
    pushdown == true일 경우 go의 모든 후손에 대한 오브젝트에 대해 탐색을 진행한다.
    */
    public static T SearchChild<T>(GameObject go, string name = null, bool pushdown = false) where T : UnityEngine.Object
    {
        if (go is null)
        {
            return null;
        }

        if (pushdown)
        {
            foreach (T e in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || e.name == name)
                {
                    return e;
                }
            }
        }
        else
        {
            foreach (Transform child in go.transform)
            {
                if (string.IsNullOrEmpty(name) || child.name == name)
                {
                    T ret = child.GetComponent<T>();
                    if (ret)
                    {
                        return ret;
                    }
                }
            }
        }

        return null;
    }

    /*
    gmaeObject를 찾고자 하는 경우 GetComponentsInChildren를 적용할 수 없으므로 Transform을 찾도록 매핑한다.
    */
    public static GameObject SearchChild(GameObject go, string name = null, bool pushdown = false)
    {
        Transform ret = SearchChild<Transform>(go, name, pushdown);
        return (ret is null ? null : ret.gameObject);
    }
}
