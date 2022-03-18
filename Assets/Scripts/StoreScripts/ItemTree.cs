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

    
    public void Awake() //�ʱ�ȭ
    {
        ResetDescription();
    }

    public void ResetDescription() // Description ���� ������ ���� �ʱ�ȭ
    {
        this.itemImage.gameObject.SetActive(false);
        this.itemPrice.text = "";
    }

    public void SetDescription(Sprite sprite, string itemGold) 
        //store���� ������ item ������ �޾Ƶ鿩�ͼ� Description ������ ����ִ� �Լ�
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.itemPrice.text = itemGold;
    }
}
