using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WaveManager : MonoBehaviour
{
    float waveTimer = 10.0f;

    [SerializeField]
    Transform[] SpawnPos;

    bool isSpawning = false;

    PhotonView PV;

    GameObject minion;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (waveTimer >= 10.0f && !isSpawning)
            {
                isSpawning = true;
                StartCoroutine("WaveSpawn");
            }
            else
                waveTimer += Time.deltaTime;
        }
    }
    
    IEnumerator WaveSpawn()
    {
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(1.0f);
            if (i < 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    PV.RPC("InstanceMinion", RpcTarget.AllBuffered, "FootmanHP", j);
                }
            }
            else
            {
                for (int j = 0; j < 3; j++)
                {
                    PV.RPC("InstanceMinion", RpcTarget.AllBuffered, "FreeLichHP", j);
                }
            } 
        }

        waveTimer = 0f;
        isSpawning = false;
    }

    [PunRPC]
    public void InstanceMinion(string name, int index)
    {
        minion = Managers.Resource.Instantiate(name, SpawnPos[index]);
    }
}
