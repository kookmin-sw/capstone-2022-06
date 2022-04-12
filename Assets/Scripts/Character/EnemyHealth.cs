using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 적군 오브제트 위의 체력바를 나타내는 스크립트
 */

public class EnemyHealth : MonoBehaviour
{
    public Slider enemySlider3D;

    EnemyStats statsScript;

    void Start()
    {
        statsScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStats>();

        enemySlider3D.maxValue = statsScript.maxHealth;
        statsScript.health = statsScript.maxHealth;
    }

    void Update()
    {
        enemySlider3D.value = statsScript.health;
    }
}
