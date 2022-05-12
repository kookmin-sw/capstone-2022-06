using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCoverable : MonoBehaviour
{
    List<Renderer> renderers;
    List<Canvas> canvases;

    void Start()
    {
        
    }

    Action<HashSet<Transform>> VisibilityChange;
}
