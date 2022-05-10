using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;

public class MonsterController : Controller
{
    int destNum;

    public enum MonsterState
    {
        IDLE,
        WALK,
        TRACE,
        ATTACK,
        DIE
    }

    MonsterState _state;

    GameObject _lockTarget;
    Transform[] wayPoints;
    NavMeshAgent _nav;

    MonsterStat _stat;

    // Start is called before the first frame update
    void Start()
    {
        _state = MonsterState.WALK;
        _stat = GetComponent<MonsterStat>();
        _stat.Initialize("CrabMonster");


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
        // 배회 알고리즘 (wayPoints 에 있는 transform들을 순서대로 목표하여 배회하도록)
        // 조건에 따른 _state 변경
    }

    void UpdateTrace()
    {
        // RUN 애니메이션 재생
        // 타겟을 SetDestination하여 추적

        // 타겟과의 거리가 일정 범위 벗어나면 target = null, _state도 WALK로 변경
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
        // 피격 시 타겟 Lock
        // _state를 trace 혹은 attack으로 변경 (거리에 의거)
    }

    public void OnHit()
    {
        // target에게 데미지를 입힘
    }
}
