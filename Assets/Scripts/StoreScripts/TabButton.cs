using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabButton : MonoBehaviour, IPointerClickHandler
{
    // item 카테고리 select 시 배경 변경 -> 아닐시 기본 배경으로 변경
    ShopManager SM;

    Image background;
    public Sprite idleImg;
    public Sprite selectedImg;
    private void Awake()
    {
        background = GetComponent<Image>();
        SM = GameObject.Find("ShopManager").GetComponent<ShopManager>();
    }
    public void Selected()
    {
        background.sprite = selectedImg;
    }
    public void DeSelected()
    {
        background.sprite = idleImg;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SM.TabSelect(this);
        }
    }
}
