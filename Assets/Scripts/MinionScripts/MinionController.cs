using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionController : MonoBehaviour
{
    protected float _speed = 8f;

    List<Transform> _wayPoints = new List<Transform>();     // List : movement path positions

    NavMeshAgent _nav;

    protected int _destNum = 0;                             // index of _wayPoints List
    protected bool _isDead = false;
    protected GameObject _lockTarget;                       // GameObject about target 

    protected float _attackRange;
    
    // minion state enum
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

        InitWayPoints();
        SettingTeam();

        InvokeRepeating("UpdateTarget", 0f, 0.25f);
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
                _nav.isStopped = false;
                if (_lockTarget != null)
                    MoveTo();
                break;
            case State.Attack:
                Animator _anim = GetComponent<Animator>();
                _anim.SetBool("OnAttack", true);

                if (_lockTarget == null)
                {
                    _anim.SetBool("OnAttack", false);
                    _state = State.Walk;
                }
                else
                    transform.LookAt(_lockTarget.transform);

                break;
        }
    }

    // register movement path positions
    protected void InitWayPoints()
    {
        Transform wayPoints = transform.parent.GetChild(0);

        for (int i = 0; i < wayPoints.childCount; i++)
            _wayPoints.Add(wayPoints.GetChild(i));
    }

    // update to next movement position 
    protected void UpdateWayPoints()
    {
        _nav.SetDestination(new Vector3(_wayPoints[_destNum].position.x, transform.position.y, _wayPoints[_destNum].position.z));

        if ((transform.position - _wayPoints[_destNum].position).magnitude < 1f && _destNum < _wayPoints.Count)
        {
            _destNum++;
            _nav.SetDestination(new Vector3(_wayPoints[_destNum].position.x, transform.position.y, _wayPoints[_destNum].position.z));
        }
    }

    // set team which minion involved
    protected void SettingTeam()
    {
        if (transform.parent.parent.name == "RTeam")
            this.gameObject.layer = LayerMask.NameToLayer("RedTeam");
        else
            this.gameObject.layer = LayerMask.NameToLayer("BlueTeam");
    }

    // update target based on array of Collider 
    protected void UpdateTarget()
    {
        int targetLayer;

        if (this.gameObject.layer == LayerMask.NameToLayer("RedTeam"))
            targetLayer = 1 << LayerMask.NameToLayer("BlueTeam");
        else
            targetLayer = 1 << LayerMask.NameToLayer("RedTeam");

        Collider[] cols = Physics.OverlapSphere(transform.position, 10.0f, targetLayer);

        if (cols.Length > 0)
        {
            for(int i = 0; i < cols.Length; i++)
            {
                if (_lockTarget == null)
                {
                    Debug.Log($"Enemy Spotted : {cols[i].gameObject.name} Found");
                    _lockTarget = cols[i].gameObject;
                    _state = State.Targetting;
                }
            }
        }
        else
        {
            _lockTarget = null;
            _state = State.Walk;
        }
    }
    
    // set destinaion to target until disance to target lower than attackRange
    protected void MoveTo()
    {
        _nav.SetDestination(_lockTarget.transform.position);

        if ((transform.position - _lockTarget.transform.position).magnitude < _attackRange)
        {
            _nav.isStopped = true;
            _state = State.Attack;
        }
    }
}
