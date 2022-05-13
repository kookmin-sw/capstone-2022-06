using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;

public class Util
{
    /// <summary>
    /// go의 자식 오브젝트 중 제네릭 T에 해당하는 컴포넌트가 있는지 찾고 없으면 null을 리턴한다.
    /// name == null일 경우 오브젝트의 이름은 상관 없이 컴포넌트 타입이 T이기만 하면 먼저 찾은 T를 반환한다.
    /// pushdown == true일 경우 go의 모든 후손에 대한 오브젝트에 대해 탐색을 진행한다.
    /// </summary>
    public static T SearchChild<T>(GameObject go, string name = null, bool pushdown = false) where T : UnityEngine.Object
    {
        if (go is null)
        {
            return null;
        }

        if (pushdown)
        {
            // inactive 오브젝트 검색을 허용합니다.
            foreach (T e in go.GetComponentsInChildren<T>(true))
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
        return (!ret ? null : ret.gameObject);
    }

    /*
    특정 이름을 가진 카메라를 검색한다.
    카메라 대수가 적을 경우에 유효하며 카메라가 많아지면 다른 방법을 고안할 것
    */
    public static Camera FindCamera(string name)
    {
        Camera[] camArray = Camera.allCameras;
        for (int i = 0; i < camArray.Length; i++)
        {
            if (name == camArray[i].name)
            {
                return camArray[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 해당 go가 "보이지 않도록" 적절한 컴포넌트를 off 합니다.
    /// </summary>
    public static void OffRenderer(Transform go)
    {
        Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
        
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        Canvas[] canvas = go.GetComponentsInChildren<Canvas>();
        foreach (Canvas canv in canvas)
        {
            canv.enabled = false;
        }
    }

    /// <summary>
    /// 해당 go가 "보이도록" 적절한 컴포넌트를 on 합니다.
    /// </summary>
    public static void OnRenderer(Transform go)
    {
        Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true;
        }

        Canvas[] canvas = go.GetComponentsInChildren<Canvas>();
        foreach (Canvas canv in canvas)
        {
            canv.enabled = true;
        }
    }

    /// <summary>
    /// 이 클라이언트의 id와 스트링으로 받은 레이어 마스크가 동일한지 확인합니다.
    /// 값을 찾지 못하면 false를 반환합니다.
    /// </summary>
    bool isSameLayer(string layerName)
    {
        object tmp;
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("actorId", out tmp))
        {
            if ((int)tmp <= 5)
            {
                return layerName == "BlueTeam";
            }
            else
            {
                return layerName == "RedTeam";
            }
        }
        else
        {
            return false;
        }
    }
}
