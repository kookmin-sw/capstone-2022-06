using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // 인게임 내의 캐릭터 상단 체력바 UI와 게임화면 UI의 체력바 동기화
    
    public Slider characterSlider3D;
    Slider characterSlider2D;

    //Stats statsScript;
    ChampionStat stat;

    void Start()
    {

        //statsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Stat>();
        stat = GameObject.FindGameObjectWithTag("Player").GetComponent<ChampionStat>();

        characterSlider2D = GetComponent<Slider>();

        characterSlider3D.maxValue = stat.Status.maxHp;
        characterSlider2D.maxValue = stat.Status.maxHp;
        stat.Status.hp = stat.Status.maxHp;
    }

    void Update()
    {
        characterSlider2D.value = stat.Status.hp;
        characterSlider3D.value = characterSlider2D.value;
    }
}
