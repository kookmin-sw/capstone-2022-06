using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorriorMinion : MinionController
{
    private void Awake()
    {
        base.Awake();

        stat = GetComponent<MinionStat>();
        stat.Initialize("WorriorMinion");
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        _attackRange = 4f;
        _templeAttackRange = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        ChangeTargetNull();
    }

    // Animation Event Call
    public void OnHit()
    {
        if (_lockTarget != null)
        {
            ObjectStat targetStat = _lockTarget.GetComponent<ObjectStat>();

            _lockTarget.GetComponent<Controller>().TakeDamage(stat.Status.atk, this.gameObject);

            if (targetStat.Status.hp <= 0)
            {
                _lockTarget = null;
                _state = State.Walk;
            }
        }
    }

    void ChangeTargetNull()
    {
        if (_lockTarget != null)
        {
            if (Vector3.Distance(transform.position, _lockTarget.transform.position) > _detectRange)
            {
                _state = State.Walk;
                _lockTarget = null;
            }
        }
    }
}
