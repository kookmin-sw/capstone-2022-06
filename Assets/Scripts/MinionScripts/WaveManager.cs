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
    
    public LayerMask initLayer;
    int localPlayerId;

    void Start()
    {
        object tmp;
        PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("actorId", out tmp);
        localPlayerId = (int)tmp; 
        PV = GetComponent<PhotonView>();
    }

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
                for (int j = 0; j < 3; j++)
                {
                    minion = PhotonNetwork.Instantiate("Prefabs/FootmanHP", SpawnPos[j].position, Quaternion.identity);
                    if (!isSameLayer())
                    {
                        Util.OffRenderer(minion);
                    }
                    PV.RPC("setLayer", RpcTarget.All);
                }
            }
            else
            {
                for (int j = 0; j < 3; j++)
                {
                    minion = PhotonNetwork.Instantiate("Prefabs/FreeLichHP", SpawnPos[j].position, Quaternion.identity);
                    if (!isSameLayer())
                    {
                        Util.OffRenderer(minion);
                    }
                    PV.RPC("setLayer", RpcTarget.All);
                }
            } 
        }

        waveTimer = 0f;
        isSpawning = false;
    }

    [PunRPC]
    void setLayer()
    {
        minion.layer = initLayer;
    }

    bool isSameLayer()
    {
        if (localPlayerId <= 5)
        {
            return initLayer.value == LayerMask.GetMask("BlueTeam");
        }
        else
        {
            return initLayer.value == LayerMask.GetMask("RedTeam");
        }
    }
}
