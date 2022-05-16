using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AreaIndicator : UI_Base
{
    Vector3 currentPos;

    public override void Init()
    {
        UpdatePosition();
    }

    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPos.y = 0.01f;
        transform.position = currentPos;
    }
}
