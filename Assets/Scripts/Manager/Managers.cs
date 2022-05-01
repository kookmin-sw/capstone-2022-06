using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance = null;
    static Managers Instance { get { Init(); return s_instance; } }

    ResourceManager _resource = new ResourceManager();
    VisibleManager _visible = new VisibleManager();
    UIManager _ui = new UIManager();
    DataManager _data = new DataManager();

    public static ResourceManager Resource { get { return Instance._resource; } }
    public static VisibleManager Visible { get { return Instance._visible; }}
    public static UIManager UI {get { return Instance._ui; }}
    public static DataManager Data { get { return Instance._data; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._data.Init();
        }
    }

    public static void Clear()
    {
        UI.Clear();
    }
}
