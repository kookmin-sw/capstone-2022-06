using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

/*
 * 플레이어의 이동 스크립트
 */

public class ClickMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    public float rotateSpeedMovement = 0.075f;
    public float rotateVelocity;

    private HeroCombat heroCombatScript;

    public GameObject clickParticle;
    private bool isPlayPart;

    PhotonView PV;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        heroCombatScript = GetComponent<HeroCombat>();
        PV = GetComponent<PhotonView>();
    }

    void Update()
    {
        if(heroCombatScript.targetedEnemy != null)
        {
            if(heroCombatScript.targetedEnemy.GetComponent<HeroCombat>() != null)
            {
                if (heroCombatScript.targetedEnemy.GetComponent<HeroCombat>().isHeroAlive)
                {
                    heroCombatScript.targetedEnemy = null;
                }
            }
        }

        // 마우스 우클릭으로 Raycast를 이용하여 클릭된 위치로 목적지 설정
        if (Input.GetMouseButton(1) && PV != null && PV.IsMine)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                Vector3 dest = hit.point;
                dest.y = 0;
                PV.RPC("moveToDestination", RpcTarget.All, dest);
            }
        }
    }

    IEnumerator ClickEffect(Vector3 dest)
    {
        // 클릭한 위치를 나타내주기 위한 파티클
        isPlayPart = true;
        GameObject effect = Instantiate(clickParticle, dest, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(effect);
        isPlayPart = false;
    }

    [PunRPC]
    void moveToDestination(Vector3 dest)
    {
        if (!isPlayPart)
        {
            StartCoroutine(ClickEffect(dest));
        }

        // 이동
        agent.SetDestination(dest);
        heroCombatScript.targetedEnemy = null;
        agent.stoppingDistance = 0;
        // 방향
        Quaternion rotationToLookAt = Quaternion.LookRotation(dest - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }
}
