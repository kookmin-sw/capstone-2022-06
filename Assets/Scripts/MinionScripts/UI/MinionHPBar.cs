using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MinionHPBar : MonoBehaviour
{
    [SerializeField] int upPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * upPos;
        transform.rotation = Camera.main.transform.rotation;
    }
}
