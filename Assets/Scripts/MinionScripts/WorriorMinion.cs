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
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    // Animation Event Call
    public void OnHit()
    {
        ObjectStat targetStat = _lockTarget.GetComponent<ObjectStat>();

        _lockTarget.GetComponent<Controller>().TakeDamage(stat.Status.atk);

        if (targetStat.Status.hp <= 0)
        {
            _lockTarget = null;
            _state = State.Walk;
        }

        Debug.Log("Hit!");
    }
}
