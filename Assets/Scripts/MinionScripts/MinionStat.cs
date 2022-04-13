using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStat : MonoBehaviour
{
    [SerializeField] int _maxHP;
    [SerializeField] int _hp;
    [SerializeField] int _atk;
    [SerializeField] int _def;

    public int MaxHP { get { return _maxHP; } set { _maxHP = value; } }
    public int HP { get { return _hp; } set { _hp = value; } }
    public int Atk { get { return _atk; } set { _atk = value; } }
    public int Def { get { return _def; } set { _def = value; } }

    private void Start()
    {
        _maxHP = 30;
        _hp = 30;
        _atk = 7;
        _def = 3;
    }
}
