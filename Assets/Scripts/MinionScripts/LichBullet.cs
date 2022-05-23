using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichBullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _atk;

    public GameObject _target;

    public float _lifeTimer;
    public float _lifeLimit;

    // Start is called before the first frame update
    void Start()
    {
        _atk = transform.parent.GetComponent<ObjectStat>().Status.atk;
    }

    // Update is called once per frame
    void Update()
    {
        _lifeTimer += Time.deltaTime;
        if (_lifeTimer >= _lifeLimit)
        {
            transform.parent.GetComponent<Controller>().isShooting = false;
            Managers.Resource.Destroy(gameObject);
        }

        if (_target != null)
        {
            Vector3 _dir = (_target.transform.position - transform.position).normalized;
            transform.position += _dir * _speed * Time.deltaTime;
            transform.forward = _dir;

            if (Vector3.Distance(transform.position, _target.transform.position) <= 1f)
            { 
                if (_target.GetComponent<Controller>() == null)
                    Debug.Log("Error!");
                else
                {
                    _target.GetComponent<Controller>().TakeDamage(_atk, transform.parent.gameObject);

                    transform.parent.GetComponent<Controller>().isShooting = false;
                    Managers.Resource.Destroy(gameObject);
                }
            }
        }
        else
        {
            transform.parent.GetComponent<Controller>().isShooting = false;
            Managers.Resource.Destroy(gameObject);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other == _target.GetComponent<CapsuleCollider>())
    //    {
    //        _target.GetComponent<Controller>().TakeDamage(_atk, transform.parent.gameObject);
    //        Managers.Resource.Destroy(gameObject);
    //    }
    //}
}
