using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 플레이어를 따라다니는 카메라 스크립트
 */

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 cameraOffset;

    [Range(0.01f, 1.0f)]
    public float smoothness = 0.5f;

    void Start()
    {
        cameraOffset = transform.position - player.position;
    }

    void Update()
    {
        Vector3 newPos = player.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
    }
}