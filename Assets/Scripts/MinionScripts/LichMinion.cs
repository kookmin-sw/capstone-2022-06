using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichMinion : MinionController
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        _attackRange = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    // Animation Event Call
}
