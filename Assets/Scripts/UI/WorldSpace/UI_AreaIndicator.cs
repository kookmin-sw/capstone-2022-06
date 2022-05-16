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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int mask = (1<< LayerMask.NameToLayer("Floor"));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            transform.position = new Vector3(hit.point.x, 0.25f, hit.point.z);
        }
    }
}
