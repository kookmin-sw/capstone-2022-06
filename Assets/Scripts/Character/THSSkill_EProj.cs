using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class THSSkill_EProj : MonoBehaviour
{
    public float damage;
    public float speed;
    public GameObject champion;
    private int targetLayer;

    void Awake()
    {
        if (gameObject.layer == LayerMask.NameToLayer("BlueTeam"))
        {
            targetLayer = LayerMask.NameToLayer("RedTeam");
        }
        else
        {
            targetLayer = LayerMask.NameToLayer("BlueTeam");
        }
    
        SetChamp();
    }

    void Start()
    {
        StartCoroutine(DestroyObject());
    }

    void Update()
    {
        gameObject.transform.TransformDirection(Vector3.forward);
        gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1.1f);
        if (gameObject)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!champion.GetPhotonView().IsMine)
            return;

        if(other.gameObject.layer == targetLayer && (other.tag == "Player" || other.tag == "Minion"))
        {
            Debug.Log("Champion Hit!");
            other.GetComponent<Controller>().TakeDamage(damage, champion);
            if (gameObject)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            if (gameObject)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    void SetChamp()
    {
        GameObject[] ar = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in ar)
        {
            if (go.GetPhotonView().IsMine && gameObject.layer == go.layer)
            {
                champion = go;
                return;
            }
        }
    }
}
