using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 위의 체력바가 항상 화면에 고정되게 보이는 스크립트

public class PlayerHealthSlider : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
