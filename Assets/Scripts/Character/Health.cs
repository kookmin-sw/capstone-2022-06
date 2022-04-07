using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // 인게임 내의 캐릭터 상단 체력바 UI와 게임화면 UI의 체력바 동기화
    
    public Slider characterSlider3D;
    Slider characterSlider2D;

    Stats statsScript;

    void Start()
    {
        statsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();

        characterSlider2D = GetComponent<Slider>();

        characterSlider3D.maxValue = statsScript.maxHealth;
        characterSlider2D.maxValue = statsScript.maxHealth;
        statsScript.health = statsScript.maxHealth;
    }

    void Update()
    {
        characterSlider2D.value = statsScript.health;
        characterSlider3D.value = characterSlider2D.value;
    }
}
