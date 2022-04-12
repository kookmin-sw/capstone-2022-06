using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 플레이어의 애니메이션 스크립트

public class PlayerAnimation : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;

    public float speed;
    float motionSmoothTime = 0.1f;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("isRun", agent.velocity.magnitude != 0);

        speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
    }
}
