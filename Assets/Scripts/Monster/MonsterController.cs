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
        // WALK �ִϸ��̼� ���
        MonsterAnimator.SetBool("IsTrace", false);
        // ��ȸ �˰��� (wayPoints �� �ִ� transform���� ������� ��ǥ�Ͽ� ��ȸ�ϵ���)
        ChangeWayPoint();
        // ���ǿ� ���� _state ����
    }

    void UpdateTrace()
    {
        // RUN �ִϸ��̼� ���
        // Ÿ���� SetDestination�Ͽ� ����

        // Ÿ�ٰ��� �Ÿ��� ���ݹ��� ���� ������ _state �� ATTACK���� ����
        // Ÿ�ٰ��� �Ÿ��� ���� ���� ����� target = null, _state�� WALK�� ����

        // TRACE ���¸� �����ð����� ������ �ϸ� _state�� WALK�� �����ϰ� target = null 
    }

    void UpdateAttack()
    {
        // ���� �ִϸ��̼� ���
        // ���� �� �Ÿ��� ����� _state�� trace�� ���� 
    }

    void UpdateDie()
    {

    }

    public override void TakeDamage(float damage)
    {
        // ������ ��� �� ����
        _stat.Status.hp -= damage;
        // �ǰ� �� Ÿ�� Lock
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

            // _state�� trace Ȥ�� attack���� ���� (�Ÿ��� �ǰ�)
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
        // target���� �������� ����
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
