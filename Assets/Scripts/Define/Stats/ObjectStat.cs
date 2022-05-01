using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectStat : MonoBehaviour
{
    [SerializeField] protected Stat stat;

    public Stat Status { get { return stat; } }

    public abstract void Initialize(string name);
}
