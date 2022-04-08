using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    //inventory: item index 별 정보를 담기위한 image, sprite, GameObject, price 배열
    public GameObject[] itemObj;
    public int[] itemPrice;

    public int maxIndex = 0;
    public bool isPull = false;
    
    public int AddIndex = 0;

    //store :item index에 등록된 정보를 inventory의 index에 담기 위한 함수
    public void AddItem(Sprite sprite, string itemprice)
    {
        if (AddIndex < 6)
        {
            for (int i = 0; i <= 5; i++)
            {
                if (itemObj[i].gameObject.activeSelf == false)
                {
                    itemObj[i].gameObject.SetActive(true);
                    itemObj[i].GetComponent<Image>().sprite = sprite;
                    itemPrice[i] = int.Parse(itemprice);
                    AddIndex++;
                    return;
                }
                else
                {
                    continue;
                }
            }
        }
        else
        {
            isPull = true;
        }
    }
    //inventory item onclick시 sell 함수
    public void SellItem(int index)
    {
        if (AddIndex >= 0 && itemObj[index].gameObject.activeSelf == true)
        {
            isPull = false;
            AddIndex--;
            itemObj[index].gameObject.SetActive(false);
            int price = itemPrice[index];
            itemPrice[index] = 0;
            Gold.playerGold += price;
        }
        else
        {
            return;
        }
    }
}
