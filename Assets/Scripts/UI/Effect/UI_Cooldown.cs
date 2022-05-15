using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Cooldown : MonoBehaviour
{
    public Image fill;
    private float maxCooldown = 90f;
    private float currentCooldown = 0f;

    public void SetMaxCooldown(in float value)
    {
        maxCooldown = value;
        UpdateFillAmount();
    }

    public void SetCurrentCooldown(in float value)
    {
        currentCooldown = value;
        UpdateFillAmount();
    }

    private void UpdateFillAmount()
    {
        fill.fillAmount = currentCooldown / maxCooldown;
    }

    void Start()
    {
        SetCurrentCooldown(90f);
        StartCoroutine(DecreaseCoolDown());
    }

    void Update()
    {
        
    }

    IEnumerator DecreaseCoolDown()
    {
        yield return new WaitUntil(() => {
            SetCurrentCooldown(currentCooldown - Time.deltaTime);
            return currentCooldown <= 0;
        });
        SetCurrentCooldown(0f);
    }
}
