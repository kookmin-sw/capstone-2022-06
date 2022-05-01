using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitButtonData : MonoBehaviour
{
    private string prefabPath, heroName;

    public string PrefabPath
    {
        get { return prefabPath; }
        set { prefabPath = value; }
    }
    
    public string HeroName
    {
        get { return heroName; }
        set { heroName = value; }
    }
}
