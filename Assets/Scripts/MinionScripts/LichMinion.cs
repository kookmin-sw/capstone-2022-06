using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichMinion : MinionController
{
    [SerializeField] Transform BulletSpawnPos;

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

        ChangeTargetNull();
    }

    // Animation Event Call
    void MakeBullet()
    {
        GameObject bullet = Managers.Resource.Instantiate("Bullet", this.gameObject.transform);
        bullet.transform.position = BulletSpawnPos.position;

        if (_lockTarget != null)
            bullet.GetComponent<LichBullet>()._target = _lockTarget;
    }

    // 추후에 플레이어 및 포탑 등이 추가되면 수정 필요
    void ChangeTargetNull()
    {
        if (_lockTarget != null)
        {
            MinionStat targetStat = _lockTarget.GetComponent<MinionStat>();
            if (targetStat.HP <= 0) _lockTarget = null;
        }
    }
}
