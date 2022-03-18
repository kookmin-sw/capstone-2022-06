using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    // Store에 있는 골드 변수 Start, Update
    public static int playerGold = 10000;
    public Text t;
    void Start() 
    {
        t.GetComponent<Text>().text = playerGold.ToString();
     }
    void Update()
    {
        t.GetComponent<Text>().text = playerGold.ToString();
    }
}
