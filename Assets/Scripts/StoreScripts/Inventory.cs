using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    //inventory: item index �� ������ ������� image, sprite, GameObject, price �迭
    public Image[] itemImages;
    public Sprite[] itemSprite;
    public GameObject[] itemObj;
    public int[] itemPrice;

    public int maxIndex = 0;
    public bool isPull = false;
    
    //public int AddIndex = 0;

    //store :item index�� ��ϵ� ������ inventory�� index�� ��� ���� �Լ�
    public void AddItem(Sprite sprite, string itemprice)
    {
        if (maxIndex <= 5)
        {
            itemSprite[maxIndex] = sprite;
            itemObj[maxIndex].gameObject.SetActive(true);
            itemObj[maxIndex].GetComponent<Image>().sprite = sprite;
            itemPrice[maxIndex] = int.Parse(itemprice);
            maxIndex += 1;
        }
        else
        {
            isPull = true;
            return;
        }
    }
    //inventory item onclick�� sell �Լ�
    public void SellItem(int index)
    {
        if (maxIndex >= 0)
        {
            isPull = false;
            itemObj[index].gameObject.SetActive(false);
            int price = itemPrice[index];
            Gold.playerGold += price;
            maxIndex -= 1;
        }
    }
}
