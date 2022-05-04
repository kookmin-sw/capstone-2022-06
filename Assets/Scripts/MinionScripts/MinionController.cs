using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MinionController : MonoBehaviourPunCallbacks, IPunObservable
{
    // minion state enum
    public enum State
    {
        Idle,
        Walk,
        Targetting,
        Attack,
        Die
    }

    // Value Attribute
    protected float _speed = 8f;
    protected int _destNum = 0;                             // index of _wayPoints List
    protected bool _isDead = false;

    protected float _attackRange;

    // Reference Attribute
    PhotonView PV;

    // protected MinionStat Stat;
    protected MinionStat stat;

    List<Transform> _wayPoints = new List<Transform>();     // List : movement path positions
    NavMeshAgent _nav;
    Collider[] targetCols;

    [SerializeField] protected GameObject _lockTarget;                       // GameObject about target 
    [SerializeField] Slider HPSlider;

    public State _state;

    // Properties
    public State ProperState { get { return _state; } set { _state = value; } }
    public MinionStat ProperStat { get { return stat; } }

    #region MonoBehaviour
    protected void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        PV = GetComponent<PhotonView>();
        stat = GetComponent<MinionStat>();
        stat.Initialize("WorriorMinion");

        SetParent();

        Managers.UI.AttachMapMarker<UI_Marker>(transform, "UI_MobMarker");
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
                _nav.isStopped = true;

                if (_lockTarget == null)
                {
                    _anim.SetBool("OnAttack", false);
                    _state = State.Walk;
                }
                else
                    transform.LookAt(_lockTarget.transform);

                break;
            case State.Die:
                StartCoroutine("UpdateDie");
                break;
        }

        if (PhotonNetwork.IsMasterClient)
            HPSlider.value = stat.Status.hp / stat.Status.maxHp;
    }
    #endregion

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


        targetCols = Physics.OverlapSphere(transform.position, 10.0f, targetLayer);

        
        Array.Sort(targetCols, delegate (Collider a, Collider b)
        {
            return Vector3.Distance(a.gameObject.transform.position, transform.position)
            .CompareTo(Vector3.Distance(b.gameObject.transform.position, transform.position));
        });

        if (targetCols.Length > 0)
        {
            for(int i = 0; i < targetCols.Length; i++)
            {
                if (_lockTarget == null)
                {
                    _lockTarget = targetCols[i].gameObject;
                    _state = State.Targetting;
                }
            }
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
        else
        {
            _nav.isStopped = false;
            _state = State.Targetting;
        }
    }

    protected void SetParent()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 5.0f);

        foreach (Collider col in cols)
        {
            if (col.gameObject.tag == "Parent")
            {
                transform.SetParent(col.gameObject.transform);
                break;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        stat.Status.hp -= damage;

        if (stat.Status.hp <= 0)
            _state = State.Die;
    }

    protected IEnumerator UpdateDie()
    {
        Animator _anim = GetComponent<Animator>();
        _anim.SetBool("IsDead", true);

        this.gameObject.layer = LayerMask.NameToLayer("Default");

        yield return new WaitForSeconds(3.0f);

        Managers.Resource.Destroy(this.gameObject);
    }

    #region RPC

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
