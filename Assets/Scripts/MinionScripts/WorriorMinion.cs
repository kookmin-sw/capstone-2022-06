using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorriorMinion : MinionController
{
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
        if (_lockTarget.gameObject.tag == "Minion")
        {
            MinionStat targetStat = _lockTarget.GetComponent<MinionStat>();

            _lockTarget.GetComponent<MinionController>().TakeDamage(Stat.Atk - targetStat.Def);

            if (targetStat.HP <= 0) _lockTarget = null;

            Debug.Log("Hit!");
        }
    }
}
