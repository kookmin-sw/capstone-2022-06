using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    //inventory: item index �� ������ ������� image, sprite, GameObject, price �迭
    public GameObject[] itemObj;
    public int[] itemPrice;

    public int maxIndex = 0;
    public bool isPull = false;
    
    public int AddIndex = 0;

    //store :item index�� ��ϵ� ������ inventory�� index�� ��� ���� �Լ�
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
    //inventory item onclick�� sell �Լ�
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
