using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUI : MonoBehaviour
{
    public GameObject StorePanel;

    bool activeStore = false;

    // 처음 시작시 상점 창이 안보이게 함
    private void Start()
    {
        StorePanel.SetActive(activeStore);    
    }


    // Tab을 눌렀을 때 켜짐
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            activeStore = !activeStore;
            StorePanel.SetActive(activeStore);
        }
    }
}
