using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCoverable : MonoBehaviour
{
    void Start()
    {
        FieldOfView.OnTargetsVisibilityChange -= VisibilityChange;
        FieldOfView.OnTargetsVisibilityChange += VisibilityChange;
    }

    void OnDestory()
    {
        FieldOfView.OnTargetsVisibilityChange -= VisibilityChange;
    }

    void VisibilityChange(HashSet<Transform> newTargets)
    {
        if (newTargets.Contains(transform))
        {
            Util.OnRenderer(gameObject);
        }
        else
        {
            Util.OffRenderer(gameObject);
        }
    }
}
