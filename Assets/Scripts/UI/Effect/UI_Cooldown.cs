using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Cooldown : MonoBehaviour
{
    public Image fill;
    private float maxCooldown = 5f;
    private float currentCooldown = 5f;

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

    void Update()
    {
        SetCurrentCooldown(currentCooldown - Time.deltaTime);

        if (currentCooldown < 0f)
        {
            currentCooldown = maxCooldown;
        }
    }
}
