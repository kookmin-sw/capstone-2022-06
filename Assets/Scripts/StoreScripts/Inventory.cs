using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public Image[] itemImages;
    public Sprite[] itemSprite;
    public GameObject[] itemObj;
    public int[] itemPrice;

    public int maxIndex = 0;
    public int AddIndex = 0;

    public void AddItem(Sprite sprite, string itemprice)
    {
        if (maxIndex <= 6)
        {
            itemSprite[maxIndex] = sprite;
            itemObj[maxIndex].gameObject.SetActive(true);
            itemObj[maxIndex].GetComponent<Image>().sprite = sprite;
            itemPrice[maxIndex] = int.Parse(itemprice);
            maxIndex += 1;
        }
        else
        {
            return;
        }
    }

    public void SellItem(int index)
    {
        if (maxIndex >= 0)
        {
            itemObj[index].gameObject.SetActive(false);
            int price = itemPrice[index];
            Gold.playerGold += price;
            maxIndex -= 1;
        }
    }
}
