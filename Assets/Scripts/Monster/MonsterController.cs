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

    public float _traceTime;
    public float _traceLimitTime;

    public enum MonsterState
    {
        IDLE,
        WALK,
        TRACE,
        ATTACK,
        DIE
    }

    public MonsterState _state;

    public List<Transform> wayPoints = new List<Transform>();
    public GameObject _lockTarget;
    NavMeshAgent _nav;
    Animator MonsterAnimator;

    MonsterStat _stat;

    // Start is called before the first frame update
    void Start()
    {
        _state = MonsterState.WALK;
        _stat = GetComponent<MonsterStat>();
        _stat.Initialize("CrabMonster");
        _nav = GetComponent<NavMeshAgent>();
        MonsterAnimator = GetComponent<Animator>();
        
        targetLayer = 1 << LayerMask.NameToLayer("RedTeam");

        InitWayPoints();

        _nav.SetDestination(wayPoints[destNum].position);

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

        if (Input.GetKeyDown(KeyCode.W))
            TakeDamage(1);
    }

    void UpdateWalk()
    {
        // WALK �ִϸ��̼� ���
        MonsterAnimator.SetBool("IsTrace", false);
        // ��ȸ �˰��� (wayPoints �� �ִ� transform���� ������� ��ǥ�Ͽ� ��ȸ�ϵ���)
        _nav.speed = _walkSpeed;
        ChangeWayPoint();

        _nav.SetDestination(wayPoints[destNum].position);
        // ���ǿ� ���� _state ����
    }

    void UpdateTrace()
    {
        _traceTime += Time.deltaTime;
        // RUN �ִϸ��̼� ���
        MonsterAnimator.SetBool("IsTrace", true);
        MonsterAnimator.SetBool("IsAttack", false);
        // Ÿ���� SetDestination�Ͽ� ����
        _nav.speed = _traceSpeed;
        _nav.SetDestination(_lockTarget.transform.position);

        // Ÿ�ٰ��� �Ÿ��� ���ݹ��� ���� ������ _state �� ATTACK���� ���� _traceTime = 0;
        // Ÿ�ٰ��� �Ÿ��� ���� ���� ����� target = null, _state�� WALK�� ���� _traceTime = 0;
        if (Vector3.Distance(transform.position, _lockTarget.transform.position) >= 50.0f)
        {
            _lockTarget = null;
            _traceTime = 0;
            _state = MonsterState.WALK;
            return;
        }

        // TRACE ���¸� �����ð����� ������ �ϸ� _state�� WALK�� �����ϰ� target = null 
        if (_traceTime >= _traceLimitTime)
        {
            _lockTarget = null;
            _traceTime = 0;
            _state = MonsterState.WALK;
        }
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
            Collider[] targetCandidates = Physics.OverlapSphere(transform.position, 20.0f, targetLayer);

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

    // �ִϸ��̼� �̺�Ʈ
    public void OnHit()
    {
        ObjectStat targetStat = _lockTarget.GetComponent<ObjectStat>();

        // target���� �������� ����
        if (_lockTarget.GetComponent<Controller>() != null)
            _lockTarget.GetComponent<Controller>().TakeDamage(_stat.Status.atk);

        if (targetStat.Status.hp <= 0)
        {
            _lockTarget = null;
            _state = MonsterState.WALK;
        }
    }

    public void InitWayPoints()
    {
        Transform tf = GameObject.Find("MonsterWayPoints").transform;

        for (int i = 0; i < tf.childCount; i++)
            wayPoints.Add(tf.GetChild(i));
    }

    public void ChangeWayPoint()
    {
        if (Vector3.Distance(transform.position, wayPoints[destNum].position) <= 1f)
        {
            ++destNum;

            if (destNum >= wayPoints.Count) destNum = 0;
        }
    }
}
