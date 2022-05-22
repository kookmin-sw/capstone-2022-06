using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class THSSkill_RProj : MonoBehaviour
{
    public float damage;
    public float speed;
    public GameObject champion;
    private int targetLayer;

    void Awake()
    {
        SetChamp();
    }

    void Start()
    {
        StartCoroutine(DestroyObject());
    }

    void Update()
    {
        gameObject.transform.TransformDirection(Vector3.down);
        gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(0.65f);
        PhotonNetwork.Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == targetLayer && (other.tag == "Player" || other.tag == "Minion"))
        {
            other.GetComponent<Controller>().TakeDamage(damage, champion);
        }
    }

    void SetChamp()
    {
        GameObject[] ar = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in ar)
        {
            if (go.GetPhotonView().IsMine)
            {
                champion = go;

                if (champion.layer == LayerMask.NameToLayer("BlueTeam"))
                {
                    targetLayer = LayerMask.NameToLayer("RedTeam");
                }
                else
                {
                    targetLayer = LayerMask.NameToLayer("BlueTeam");
                }
                return;
            }
        }
    }
}

