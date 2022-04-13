using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인게임 씬에서 나타나는 UI의 부모격 클래스.
/// 인게임에서 띄워지는 UI 클래스는 모두 이 클래스를 상속받아야 합니다.
/// </summary>
public class UI_Scene : UI_Base
{
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }
}
