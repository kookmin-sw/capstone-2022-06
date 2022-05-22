using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class MinionController : Controller, IPunObservable
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
    protected float _detectRange = 15f;

    // Reference Attribute
    PhotonView PV;

    // protected MinionStat Stat;
    protected MinionStat stat;

    List<Transform> _wayPoints = new List<Transform>();     // List : movement path positions
    NavMeshAgent _nav;
    Animator _anim;
    Collider[] targetCols;

    [SerializeField] protected GameObject _lockTarget;                       // GameObject about target 
    [SerializeField] GameObject _currentAttacker;
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
        _anim = GetComponent<Animator>();

        SetParent();

        Managers.UI.AttachMapMarker<UI_Marker>(transform, "UI_MobMarker");
    }

    
    // Start is called before the first frame update
    protected void Start()
    {
        _state = State.Walk;

        InitWayPoints();
        SettingTeam();

        InvokeRepeating("UpdateTarget", 0f, 0.1f);
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
                _anim.SetBool("OnAttack", false);
                UpdateWayPoints();
                break;
            case State.Targetting:
                _nav.isStopped = false;
                if (_lockTarget != null)
                    MoveTo();
                break;
            case State.Attack:
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

        if (_state != State.Targetting)
        {
            targetCols = Physics.OverlapSphere(transform.position, _detectRange, targetLayer);
            
            IEnumerable<Collider> query = from target in targetCols
                                          orderby target.GetComponent<ObjectStat>().Status.priority,
                                                  Vector3.Distance(target.gameObject.transform.position, transform.position)
                                          select target;

            if (query.Count() > 0)
            {
                foreach (Collider col in query)
                {
                    if (!col.gameObject.GetComponent<Targetable>())
                    {
                        continue;
                    }

                    if (Vector3.Distance(transform.position, col.gameObject.transform.position) <= _detectRange)
                    {
                        _lockTarget = col.gameObject;
                        _state = State.Targetting;
                        break;
                    }
                }
            }

            Array.Clear(targetCols, 0, targetCols.Length);
        }
    }
    
    // set destinaion to target until disance to target lower than attackRange
    protected void MoveTo()
    {
        if (_lockTarget == null)
        {
            _state = State.Walk;
            return;
        }

        _nav.SetDestination(_lockTarget.transform.position);

        if (Vector3.Distance(transform.position, _lockTarget.transform.position) <= _attackRange)
        {
            _nav.isStopped = true;
            _nav.ResetPath();
            _state = State.Attack;
        }
        else
        {
            _nav.isStopped = false;
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

    public override void TakeDamage(float damage, GameObject attacker = null)
    {
        if (attacker != null)
            _currentAttacker = attacker;

        stat.Status.hp -= damage;

        if (stat.Status.hp <= 0)
        {
            _state = State.Die;

            if (attacker != null && attacker.tag == "Player")
            {
                attacker.GetComponent<ChampionStat>().Status.gold += stat.Status.GivingGold;
            }

            return;
        }
    }

    protected IEnumerator UpdateDie()
    {
        Animator _anim = GetComponent<Animator>();
        _anim.SetBool("IsDead", true);

        if (!_isDead)
        {
            GiveExp();
            _isDead = true;

            if (_currentAttacker.gameObject.tag == "Player")
            {
                _currentAttacker.GetComponent<ObjectStat>().Status.gold += stat.Status.GivingGold;
            }
        }

        this.gameObject.layer = LayerMask.NameToLayer("Default");

        yield return new WaitForSeconds(3.0f);

        if (gameObject != null)
            Managers.Resource.Destroy(this.gameObject);
    }

    protected void GiveExp()
    {
        int targetLayer;

        if (this.gameObject.layer == LayerMask.NameToLayer("RedTeam"))
            targetLayer = 1 << LayerMask.NameToLayer("BlueTeam");
        else
            targetLayer = 1 << LayerMask.NameToLayer("RedTeam");

        Collider[] cols = Physics.OverlapSphere(transform.position, 100.0f, targetLayer);

        
        List<GameObject> players = new List<GameObject>();
        foreach (Collider col in cols)
        {
            if (col.gameObject.tag == "Player")
            {
                players.Add(col.gameObject);
            }
        }

        if (players.Count > 0)
        {
            foreach (GameObject player in players)
            {
                player.GetComponent<ChampionStat>().Status.exp += (stat.Status.GivingExp / players.Count);
            }
        }
        else
            return;
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
