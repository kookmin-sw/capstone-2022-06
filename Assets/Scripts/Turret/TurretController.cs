using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    int targetLayer;
    [SerializeField] float _detectRange;
    [SerializeField] float _attackInterval;
    float _attackTimer;

    [SerializeField] GameObject _lockTarget;
    Collider[] targetCandidates;

    TurretStat stat;

    [SerializeField] Transform bulletSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        stat = GetComponent<TurretStat>();
        stat.Initialize("Turret");

        if (this.gameObject.layer == LayerMask.NameToLayer("RedTeam"))
            targetLayer = 1 << LayerMask.NameToLayer("BlueTeam");
        else
            targetLayer = 1 << LayerMask.NameToLayer("RedTeam");

        InvokeRepeating("UpdateTarget", 0f, 0.25f);
        _attackTimer = _attackInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (_lockTarget != null)
        {
            ShootBullet();
            _attackTimer += Time.deltaTime;
        }
        else
            _attackTimer = 0f;
    }

    void UpdateTarget()
    {
        targetCandidates = Physics.OverlapSphere(transform.position, _detectRange, targetLayer);

        // 추후에 오브젝트들의 우선순위를 기반으로 정렬하기 위해 수정이 필요
        Array.Sort(targetCandidates, delegate (Collider a, Collider b)
        {
            return Vector3.Distance(a.gameObject.transform.position, transform.position)
            .CompareTo(Vector3.Distance(b.gameObject.transform.position, transform.position));
        });

        if (targetCandidates.Length > 0)
        {
            for (int i = 0; i < targetCandidates.Length; i++)
            {
                if (_lockTarget == null)
                {
                    _lockTarget = targetCandidates[i].gameObject;
                }
            }
        }
    }

    void MakeBullet()
    {
        GameObject bullet = Managers.Resource.Instantiate("Bullet", this.gameObject.transform);
        bullet.transform.position = bulletSpawnPos.position;

        bullet.GetComponent<LichBullet>()._target = _lockTarget;
    }

    void ShootBullet()
    {
        if (_attackTimer >= _attackInterval)
        {
            MakeBullet();
            _attackTimer = 0f;
        }
    }
}
