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

            targetStat.HP -= (Stat.Atk - targetStat.Def);

            if (targetStat.HP <= 0)
                targetStat.gameObject.GetComponent<MinionController>().ProperState = State.Die;

            Debug.Log("Hit!");
        }
    }
}
