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
        localPlayerId = (int)Util.GetLocalPlayerId();
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
        string[] minionPath = new string[] {"Prefabs/FootmanHP", "Prefabs/FreeLichHP"};
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(2.0f);

            for (int j = 0; j < 3; j++)
            {
                minion = PhotonNetwork.Instantiate(minionPath[i / 2], SpawnPos[j].position, Quaternion.identity);
                int minionId = minion.GetPhotonView().ViewID;
                PV.RPC("InitLayerController", RpcTarget.All, minionId);

                if (IsSameLayer())
                {
                    FieldOfView fov = minion.GetOrAddComponent<FieldOfView>();
                    fov.viewRadius = 15f;
                    fov.obstacleMask.value = LayerMask.GetMask("Obstacle");
                    fov.allyMask.value = initLayer.value;
    
                    if (initLayer == LayerMask.GetMask("BlueTeam"))
                    {
                        fov.opposingMask.value = LayerMask.GetMask("RedTeam");
                    }
                    else
                    {
                        fov.opposingMask.value = LayerMask.GetMask("BlueTeam");
                    }
                }
            }
        }

        waveTimer = 0f;
        isSpawning = false;
    }

    [PunRPC]
    void setLayer()
    {
        minion.layer = (int)Mathf.Log(initLayer.value, 2);
    }

    bool IsSameLayer()
    {
        return initLayer == LayerMask.GetMask(Util.GetMyLayerString());
    }

    [PunRPC]
    void InitLayerController(int id)
    {
        PhotonView.Find(id).gameObject.GetOrAddComponent<LayerController>().SetLayer(LayerMask.LayerToName((int)Mathf.Log(initLayer.value, 2)));
    }
}
