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
        // WALK �ִϸ��̼� ���
        // ��ȸ �˰��� (wayPoints �� �ִ� transform���� ������� ��ǥ�Ͽ� ��ȸ�ϵ���)
        // ���ǿ� ���� _state ����
    }

    void UpdateTrace()
    {
        // RUN �ִϸ��̼� ���
        // Ÿ���� SetDestination�Ͽ� ����

        // Ÿ�ٰ��� �Ÿ��� ���� ���� ����� target = null, _state�� WALK�� ����
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
        // �ǰ� �� Ÿ�� Lock
        // _state�� trace Ȥ�� attack���� ���� (�Ÿ��� �ǰ�)
    }

    public void OnHit()
    {
        // target���� �������� ����
    }
}
