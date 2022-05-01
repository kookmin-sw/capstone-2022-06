using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichBullet : MonoBehaviour
{
    [SerializeField] float _speed;
    float _atk;

    public GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        _atk = transform.parent.GetComponent<MinionStat>().Status.atk;
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            Vector3 _dir = (_target.transform.position - transform.position).normalized;
            transform.position += _dir * _speed * Time.deltaTime;
            transform.forward = _dir;
        }
        else
            Managers.Resource.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Minion")
        {
            if (other == _target.GetComponent<CapsuleCollider>())
            {
                Debug.Log("Bullet hits minion!!");
                _target.GetComponent<MinionController>().TakeDamage(_atk - _target.GetComponent<MinionStat>().Status.defense);
                Managers.Resource.Destroy(gameObject);
            }

        }
    }
}
