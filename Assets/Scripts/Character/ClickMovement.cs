using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using UnityEngine.EventSystems;

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

    ShopManager SM;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0.2f;
        heroCombatScript = GetComponent<HeroCombat>();
        PV = GetComponent<PhotonView>();
        SM = GameObject.Find("ShopManager").GetComponent<ShopManager>();
    }

    void Update()
    {
        // 마우스 우클릭으로 Raycast를 이용하여 클릭된 위치로 목적지 설정 + 상점 체크
        if (Input.GetMouseButton(1) && PV != null && PV.IsMine)
        {
            if (!IsPointerOverUIObject())
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
                {
                    if (hit.collider.gameObject.CompareTag("Shop"))
                    {
                        if (SM != null && !SM.panelactive)
                        {
                            if (SM.IsPlayerInShopArea()) //상점 범위 안일 때만 상호작용
                            {
                                SM.panelactive = !SM.panelactive;
                                SM.StorePanel.SetActive(true);
                            }
                        }
                    }
                    else
                    {
                        agent.isStopped = false;
                        Vector3 dest = hit.point;
                        dest.y = 0;
                        PV.RPC("moveToDestination", RpcTarget.All, dest);
                    }
                }
            }
        }
        if (Input.GetMouseButton(0) && PV != null && PV.IsMine)
        {
            if (!IsPointerOverUIObject())
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject.CompareTag("Shop"))
                    {
                        if (SM != null && !SM.panelactive)
                        {
                            if (SM.IsPlayerInShopArea()) //상점 범위 안일 때만 상호작용
                            {
                                SM.panelactive = !SM.panelactive;
                                SM.StorePanel.SetActive(true);
                            }
                        }
                    }
                }
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

    /// <summary>
    /// 주어진 dest로 챔피언을 이동시키는 RPC 메서드입니다.
    /// </summary>
    [PunRPC]
    void moveToDestination(Vector3 dest)
    {
        if (!isPlayPart)
        {
            StartCoroutine(ClickEffect(dest));
        }

        // 이동
        agent.SetDestination(dest);
        // 방향
        Quaternion rotationToLookAt = Quaternion.LookRotation(dest - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }
    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.layer == 5) //5 = UI layer
            {
                return true;
            }
        }

        return false;
    }
}
