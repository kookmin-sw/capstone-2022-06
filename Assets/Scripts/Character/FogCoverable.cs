using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCoverable : MonoBehaviour
{
    void Start()
    {

    }

    void VisibilityChange(HashSet<Transform> newTargets) {
        if (newTargets.Contains(transform))
        {
            Util.OnRenderer(gameObject);
        }
    }
}
