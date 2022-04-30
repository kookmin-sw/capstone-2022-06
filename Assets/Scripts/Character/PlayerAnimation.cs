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

    Stats stats;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        stats = GetComponent<Stats>();
    }

    void Update()
    {
        anim.SetBool("isRun", agent.velocity.magnitude != 0);

        speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);

        // 공격 속도
        anim.SetFloat("attackSpeed", stats.attackSpeed);
    }
}
