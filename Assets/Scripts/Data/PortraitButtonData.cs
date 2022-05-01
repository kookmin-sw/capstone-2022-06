using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PreparationScene에서 선택한 영웅의 게임 진행에 필요한 정보를 저장하는 클래스 입니다.
/// </summary>
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
