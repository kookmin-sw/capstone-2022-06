using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WaveManager : MonoBehaviour
{
    float waveTimer = 60.0f;

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
            if (waveTimer >= 60.0f && !isSpawning)
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
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(1.0f);
            if (i < 2)
            {
                for (int j = 0; j < 2; j++)
                {
                    PhotonNetwork.Instantiate("Prefabs/FootmanHP", SpawnPos[j].position, Quaternion.identity);
                }
            }
            else
            {
                for (int j = 0; j < 2; j++)
                {
                    PhotonNetwork.Instantiate("Prefabs/FreeLichHP", SpawnPos[j].position, Quaternion.identity);
                }
            } 
        }

        waveTimer = 0f;
        isSpawning = false;
    }
}
