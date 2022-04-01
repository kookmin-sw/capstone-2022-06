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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waveTimer >= 10.0f && !isSpawning)
        {
            isSpawning = true;
            StartCoroutine("WaveSpawn");
        }
        else
            waveTimer += Time.deltaTime;
    }
    
    IEnumerator WaveSpawn()
    {
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(1.0f);
            if (i < 3)
            {
                for (int j = 0; j < 3; j++)
                    Managers.Resource.PunInstantiate("FootmanHP", SpawnPos[j]);
            }
            else
            {
                for (int j = 0; j < 3; j++)
                    Managers.Resource.PunInstantiate("FreeLichHP", SpawnPos[j]);
            } 
        }

        waveTimer = 0f;
        isSpawning = false;
    }
}
