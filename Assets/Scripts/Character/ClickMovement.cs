using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    public float rotateSpeedMovement = 0.1f;
    float rotateVelocity;

    private Camera camera;
    private Animator animator;

    private bool isMove;
    private Vector3 destination;

    public GameObject clickParticle;

    private bool isPlayPart;

    private void Awake()
    {
        camera = Camera.main;
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    void Update()
    {
        // 마우스 우클릭으로 Raycast를 이용하여 클릭된 위치로 목적지 설정
        if(Input.GetMouseButton(1))
        {
            RaycastHit hit;
            if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if (!isPlayPart && hit.transform.CompareTag("Floor"))
                    StartCoroutine(ClickEffect(hit.point));
                SetDestination(hit.point);
            }
        }

        LookMoveDirection();
    }

    private void SetDestination(Vector3 dest)
    {
        // 클릭된 목적지로 이동
        agent.SetDestination(dest);
        destination = dest;
        isMove = true;
        animator.SetBool("isMove", true);
    }

    private void LookMoveDirection()
    {
        // 목적지를 바라보게 캐릭터의 rotation조정
        if (agent.velocity.magnitude == 0f)
        {
            isMove = false;
            animator.SetBool("isMove", false);
            return;
        }

        if (isMove)
        {
            var dir = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z) - transform.position;
            animator.transform.forward = dir;
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
}
