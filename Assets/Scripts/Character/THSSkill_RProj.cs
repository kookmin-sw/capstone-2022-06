using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class THSSkill_RProj : MonoBehaviour
{
    public float damage;
    public float speed;

    void Update()
    {
        StartCoroutine(DestroyObject());

        gameObject.transform.TransformDirection(Vector3.down);
        gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(0.65f);
        Destroy(gameObject);
    }
}

