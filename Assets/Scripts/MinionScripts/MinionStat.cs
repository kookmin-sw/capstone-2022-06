using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStat : MonoBehaviour
{
    [SerializeField] float _maxHP;
    [SerializeField] float _hp;
    [SerializeField] float _atk;
    [SerializeField] float _def;

    public float MaxHP { get { return _maxHP; } set { _maxHP = value; } }
    public float HP { get { return _hp; } set { _hp = value; } }
    public float Atk { get { return _atk; } set { _atk = value; } }
    public float Def { get { return _def; } set { _def = value; } }

    private void Start()
    {
        _maxHP = 30;
        _hp = 30;
        _atk = 7;
        _def = 3;
    }
}
