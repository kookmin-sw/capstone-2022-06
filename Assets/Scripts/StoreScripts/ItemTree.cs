using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTree : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private Text itemPrice;

    
    public void Awake() //초기화
    {
        ResetDescription();
    }

    public void ResetDescription() // Description 영역 아이템 정보 초기화
    {
        this.itemImage.gameObject.SetActive(false);
        this.itemPrice.text = "";
    }

    public void SetDescription(Sprite sprite, string itemGold) 
        //store에서 선택한 item 정보를 받아들여와서 Description 영역에 띄워주는 함수
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.itemPrice.text = itemGold;
    }
}
