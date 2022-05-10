using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemInfo : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    ShopManager SM;
    public Item item;

    public int actualprice;
    float time;
    float doubleclickdetecttime = 0.5f;

    List<InventorySlot> RecursiveCheckingList;
    private void Start()
    {
        time = doubleclickdetecttime;
        SM = GameObject.Find("ShopManager").GetComponent<ShopManager>();
        actualprice = item.price;
        if (item.itemname == "체력 물약") SM.ItemSelect(this);
    }
    void Update()
    {
        if (time < doubleclickdetecttime) time += Time.deltaTime;

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (time >= doubleclickdetecttime)
            {
                time = 0f;
                SM.ItemSelect(this);
            }
            else
            {
                time = doubleclickdetecttime;
                SM.ItemBuy(item, actualprice);
            }
        }
            
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            SM.ItemBuy(item, actualprice);
        }

    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        SM.MouseOnItemInfo = this;
        SM.mouseentered = true;
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        SM.mouseentered = false;
    }

    public void CalculateActualPrice()
    {
        if (item != null)
        {
            actualprice = RecursiveCalculatePrice(item);
            gameObject.transform.GetChild(1).GetComponent<Text>().text = actualprice.ToString();
        }
    }

    int RecursiveCalculatePrice(Item item)
    {
        RecursiveCheckingList = new List<InventorySlot>();
        return item.price - RecursiveHelper(item);
    }
    int RecursiveHelper(Item item)
    {
        int ret = 0;
        if (item.loweritems.Count > 0)
        {
            foreach (Item loweritem in item.loweritems)
            {
                bool exist = false;
                foreach (InventorySlot invslot in SM.InventorySlots)
                {
                    if (invslot.occupied && loweritem.itemname == invslot.item.itemname && !RecursiveCheckingList.Contains(invslot))
                    {
                        RecursiveCheckingList.Add(invslot);
                        ret += invslot.item.price;
                        exist = true;
                        break;
                    }
                }
                if (!exist) ret += RecursiveHelper(loweritem);
            }
        }
        return ret;
    }

    public void OnClick()
    {
        SM.ItemSelect(this);
    }
}
