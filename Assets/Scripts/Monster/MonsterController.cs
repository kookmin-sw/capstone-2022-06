using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;

public class MonsterController : Controller
{
    int destNum = 0;
    int targetLayer;

    public float _walkSpeed;
    public float _traceSpeed;
    public float _attackRange;

    public enum MonsterState
    {
        IDLE,
        WALK,
        TRACE,
        ATTACK,
        DIE
    }

    MonsterState _state;

    public Transform[] wayPoints;
    GameObject _lockTarget;
    NavMeshAgent _nav;
    Animator MonsterAnimator;

    MonsterStat _stat;

    // Start is called before the first frame update
    void Start()
    {
        _state = MonsterState.WALK;
        _stat = GetComponent<MonsterStat>();
        _stat.Initialize("CrabMonster");
        targetLayer = 1 << LayerMask.NameToLayer("BlueTeam") | LayerMask.NameToLayer("RedTeam");

        _nav.SetDestination(wayPoints[destNum].position);

        MonsterAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(_state)
        {
            case MonsterState.IDLE:
                break;
            case MonsterState.WALK:
                UpdateWalk();
                break;
            case MonsterState.TRACE:
                UpdateTrace();
                break;
            case MonsterState.ATTACK:
                UpdateAttack();
                break;
        }
    }

    void UpdateWalk()
    {
        // WALK 애니메이션 재생
        MonsterAnimator.SetBool("IsTrace", false);
        // 배회 알고리즘 (wayPoints 에 있는 transform들을 순서대로 목표하여 배회하도록)
        ChangeWayPoint();
        // 조건에 따른 _state 변경
    }

    void UpdateTrace()
    {
        // RUN 애니메이션 재생
        // 타겟을 SetDestination하여 추적

        // 타겟과의 거리가 공격범위 내에 들어오면 _state 를 ATTACK으로 변경
        // 타겟과의 거리가 일정 범위 벗어나면 target = null, _state도 WALK로 변경

        // TRACE 상태를 일정시간동안 유지만 하면 _state를 WALK로 변경하고 target = null 
    }

    void UpdateAttack()
    {
        // 공격 애니메이션 재생
        // 공격 중 거리가 벗어나면 _state를 trace로 변경 
    }

    void UpdateDie()
    {

    }

    public override void TakeDamage(float damage)
    {
        // 데미지 계산 후 적용
        _stat.Status.hp -= damage;
        // 피격 시 타겟 Lock
        if (_state == MonsterState.WALK)
        {
            Collider[] targetCandidates = Physics.OverlapSphere(transform.position, 7.0f, targetLayer);

            Array.Sort(targetCandidates, delegate (Collider a, Collider b)
            {
                return Vector3.Distance(a.gameObject.transform.position, transform.position)
                .CompareTo(Vector3.Distance(b.gameObject.transform.position, transform.position));
            });

            foreach(Collider col in targetCandidates)
            {
                if (col.gameObject.tag == "Player")
                {
                    _lockTarget = col.gameObject;
                    break;
                }
            }

            // _state를 trace 혹은 attack으로 변경 (거리에 의거)
            if (_lockTarget != null)
            {
                if (Vector3.Distance(transform.position, _lockTarget.transform.position) <= _attackRange)
                    _state = MonsterState.ATTACK;
                else
                    _state = MonsterState.TRACE;
            }
        }
    }

    public void OnHit()
    {
        // target에게 데미지를 입힘
    }

    public void ChangeWayPoint()
    {
        if (Vector3.Distance(transform.position, wayPoints[destNum].position) <= 1f)
        {
            ++destNum;

            if (destNum >= wayPoints.Length) destNum = 0;

            _nav.SetDestination(wayPoints[destNum].position);
        }
    }
}
