using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionController : MonoBehaviour
{
    protected float _speed = 5f;

    List<Transform> _wayPoints = new List<Transform>();

    NavMeshAgent _nav;

    protected int _destNum = 0;
    protected bool _isDead = false;
    protected GameObject _lockTarget;

    protected float _attackRange;

    public enum State
    {
        Idle,
        Walk,
        Targetting,
        Attack
    }

    public State _state;

    protected void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        _state = State.Walk;

        // transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        InitWayPoints();
        //_nav.SetDestination(new Vector3(_wayPoints[0].position.x, transform.position.y, _wayPoints[0].position.z));

        // InvokeRepeating("UpdateTarget", 0f, 0.25f);

    }

    // Update is called once per frame
    protected void Update()
    {
        switch (_state)
        {
            case State.Idle:
                break;
            case State.Walk:
                _nav.isStopped = false;
                UpdateWayPoints();
                break;
            case State.Targetting:
                // TODO
                break;
            case State.Attack:
                // TODO
                break;
        }
    }

    protected void InitWayPoints()
    {
        Transform wayPoints = transform.parent.GetChild(0);

        for (int i = 0; i < wayPoints.childCount; i++)
            _wayPoints.Add(wayPoints.GetChild(i));
    }

    protected void UpdateWayPoints()
    {
        _nav.SetDestination(new Vector3(_wayPoints[_destNum].position.x, transform.position.y, _wayPoints[_destNum].position.z));

        if ((transform.position - _wayPoints[_destNum].position).magnitude < 1f && _destNum < _wayPoints.Count)
        {
            _destNum++;
            _nav.SetDestination(new Vector3(_wayPoints[_destNum].position.x, transform.position.y, _wayPoints[_destNum].position.z));
        }
    }

    //protected void UpdateTarget()
    //{
        
    //}

    //protected void MoveTo()
    //{

    //}
}
