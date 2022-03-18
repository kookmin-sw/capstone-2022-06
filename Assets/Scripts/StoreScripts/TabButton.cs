using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabButton : MonoBehaviour
{
    // item 카테고리 select 시 배경 변경 -> 아닐시 기본 배경으로 변경
    Image background;
    public Sprite idleImg;
    public Sprite selectedImg;
    private void Awake()
    {
        background = GetComponent<Image>();
    }
    public void Selected()
    {
        background.sprite = selectedImg;
    }
    public void DeSelected()
    {
        background.sprite = idleImg;
    }
}
