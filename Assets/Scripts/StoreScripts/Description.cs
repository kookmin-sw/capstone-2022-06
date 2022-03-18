using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Description : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private Text itemPrice, title, description;

    public void Awake() //초기화
    {
        ResetDescription();
    }

    public void ResetDescription() // 실행시 item들을 초기화를 위한 함수
    {
        this.itemImage.gameObject.SetActive(false);
        this.itemPrice.text = "";
        this.title.text = "";
        this.description.text = "";
    }

    public void SetDescription(Sprite sprite, string itemGold, string itemName, string itemDescription) 
        // 선택한 item의 지정된 정보를 Description에 불러오기 
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.itemPrice.text = itemGold;
        this.title.text = itemName;
        this.description.text = itemDescription;
    }
}
