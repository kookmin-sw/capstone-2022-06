using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TurretController : Controller
{
    int targetLayer;
    [SerializeField] float _detectRange;
    [SerializeField] float _attackInterval;
    float _attackTimer;

    [SerializeField] GameObject _lockTarget;
    [SerializeField] Slider HPSlider;
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

        InvokeRepeating("UpdateTarget", 0f, 0.1f);
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

        ChangeTargetNull();
        
        HPSlider.value = stat.Status.hp / stat.Status.maxHp;
    }

    void UpdateTarget()
    {
        targetCandidates = Physics.OverlapSphere(transform.position, _detectRange, targetLayer);

        IEnumerable<Collider> query = from target in targetCandidates
                                        orderby target.GetComponent<ObjectStat>().Status.priority,
                                                Vector3.Distance(target.gameObject.transform.position, transform.position)
                                        select target;
        
        if (query.Count() > 0)
            _lockTarget = query.ElementAt<Collider>(0).gameObject;

        Array.Clear(targetCandidates, 0, targetCandidates.Length);
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

    void ChangeTargetNull()
    {
        if (_lockTarget != null)
        {
            ObjectStat targetStat = _lockTarget.GetComponent<ObjectStat>();
            if (targetStat.Status.hp <= 0) _lockTarget = null;
        }
    }

    public override void TakeDamage(float damage, GameObject attacker = null)
    {
        stat.Status.hp -= damage;

        //if (stat.Status.hp <= 0)
        //    _state = State.Die;
    }

    #region PhotonSerialize
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(HPSlider.value);
        }
        else
        {
            HPSlider.value = (float)stream.ReceiveNext();
        }
    }
    #endregion
}
