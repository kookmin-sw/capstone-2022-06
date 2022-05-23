using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichMinion : MinionController
{
    [SerializeField] Transform BulletSpawnPos;

    private void Awake()
    {
        base.Awake();

        stat = GetComponent<MinionStat>();
        stat.Initialize("LichMinion");
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        _attackRange = 12f;
        _templeAttackRange = 25f;
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
        isShooting = true;

        if (_lockTarget != null)
            bullet.GetComponent<LichBullet>()._target = _lockTarget;
    }

    // 추후에 플레이어 및 포탑 등이 추가되면 수정 필요
    void ChangeTargetNull()
    {
        if (_lockTarget != null)
        {
            ObjectStat targetStat = _lockTarget.GetComponent<ObjectStat>();
            if (targetStat.Status.hp <= 0 || Vector3.Distance(transform.position, _lockTarget.transform.position) > _detectRange)
            {
                _state = State.Walk;
                _lockTarget = null;
            }
        }
    }
}
