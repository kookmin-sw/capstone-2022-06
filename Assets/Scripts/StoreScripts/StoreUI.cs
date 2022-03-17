using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUI : MonoBehaviour
{
    public GameObject StorePanel;

    bool activeStore = false;

    private void Start()
    {
        StorePanel.SetActive(activeStore);
        
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            activeStore = !activeStore;
            StorePanel.SetActive(activeStore);
        }
    }
}
