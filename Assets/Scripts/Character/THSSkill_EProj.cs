using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class THSSkill_EProj : MonoBehaviour
{
    public float damage;
    public float speed;

    void Update()
    {
        StartCoroutine(DestroyObject());

        gameObject.transform.TransformDirection(Vector3.forward);
        gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(0.55f);
        if (gameObject)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == Util.GetEnemyLayer() && (other.tag == "Player" || other.tag == "Minion"))
        {
            other.GetComponent<Controller>().TakeDamage(damage, this.gameObject);
            if (gameObject)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
