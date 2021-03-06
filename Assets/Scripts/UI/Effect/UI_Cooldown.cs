using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Cooldown : MonoBehaviour
{
    public Image fill;
    private float maxCooldown = 30f;
    private float currentCooldown = 0f;

    /// <summary> 최대 쿨타임을 변경 </summary>
    public void SetMaxCooldown(in float value)
    {
        maxCooldown = value;
        UpdateFillAmount();
    }

    /// <summary> 현재 쿨타임을 변경 </summary>
    public void SetCurrentCooldown(in float value)
    {
        currentCooldown = value;
        UpdateFillAmount();
    }

    /// <summary> 현재 쿨타임을 최대로 채우는 메서드 </summary>
    public void FillCurrentCooldown()
    {
        SetCurrentCooldown(maxCooldown);
        StartCoroutine(ReduceCooldown());
    }

    /// <summary> 현재 쿨타임에 따라 시각 효과를 반영하는 메서드 </summary>
    private void UpdateFillAmount()
    {
        fill.fillAmount = currentCooldown / maxCooldown;
    }

    void Start()
    {
        SetCurrentCooldown(maxCooldown);
        StartCoroutine(ReduceCooldown());
    }

    public IEnumerator ReduceCooldown()
    {
        yield return new WaitUntil(() => {
            SetCurrentCooldown(currentCooldown - Time.deltaTime);
            return currentCooldown <= 0;
        });
        SetCurrentCooldown(0f);
    }

    public bool IsCooldownFinished()
    {
        return currentCooldown == 0f;
    }
}
