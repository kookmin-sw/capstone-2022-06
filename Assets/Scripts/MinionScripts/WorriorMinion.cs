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
}