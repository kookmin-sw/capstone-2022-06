using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    //inventory: item index 별 정보를 담기위한 image, sprite, GameObject, price 배열
    public Image[] itemImages;
    public Sprite[] itemSprite;
    public GameObject[] itemObj;
    public int[] itemPrice;

    public int maxIndex = 0;
    public bool isPull = false;
    
    //public int AddIndex = 0;

    //store :item index에 등록된 정보를 inventory의 index에 담기 위한 함수
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
    //inventory item onclick시 sell 함수
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
