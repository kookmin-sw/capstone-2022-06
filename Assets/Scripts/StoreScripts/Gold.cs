using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
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
