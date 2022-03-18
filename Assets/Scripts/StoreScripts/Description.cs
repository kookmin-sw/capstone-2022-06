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

    public void Awake() //�ʱ�ȭ
    {
        ResetDescription();
    }

    public void ResetDescription() // ����� item���� �ʱ�ȭ�� ���� �Լ�
    {
        this.itemImage.gameObject.SetActive(false);
        this.itemPrice.text = "";
        this.title.text = "";
        this.description.text = "";
    }

    public void SetDescription(Sprite sprite, string itemGold, string itemName, string itemDescription) 
        // ������ item�� ������ ������ Description�� �ҷ����� 
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.itemPrice.text = itemGold;
        this.title.text = itemName;
        this.description.text = itemDescription;
    }
}
