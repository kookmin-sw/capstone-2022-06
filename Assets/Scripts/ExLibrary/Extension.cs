using UnityEngine;

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
}
