using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� ���� ü�¹ٰ� �׻� ȭ�鿡 �����ǰ� ���̴� ��ũ��Ʈ

public class PlayerHealthSlider : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation = Camera.main.transform.rotation;
    }
}
