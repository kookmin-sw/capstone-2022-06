using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TreeSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int slotnum;
    ShopManager SM;
    public Item item;
    public Image SlotImage;
    public Text SlotPriceText;
    bool owned = false;
    Color NotOwnedCol;
    Color OwnedCol;

    float time;
    float doubleclickdetecttime = 0.5f;

    public int actualprice;

    List<InventorySlot> RecursiveCheckingList;

    private void Awake()
    {
        SM = GameObject.Find("ShopManager").GetComponent<ShopManager>();
        NotOwnedCol = new Color(1f, 1f, 1f, 1f);
        OwnedCol = new Color(102f / 255f, 102f / 255f, 102f / 255f, 1f);
    }
    private void Start()
    {
        time = doubleclickdetecttime;
    }

    // Update is called once per frame
    void Update()
    {
        if (time < doubleclickdetecttime) time += Time.deltaTime;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (slotnum == 0)
            {
                if (time >= doubleclickdetecttime)
                {
                    time = 0f;
                }
                else
                {
                    time = doubleclickdetecttime;
                    SM.ItemBuy(item, actualprice);
                }
            }
            else
            {
                SM.ItemSelect(item.iteminfo);
            }

        }

        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            SM.ItemBuy(item, actualprice);
        }

    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (item != null)
        {
            SM.MouseOnItemInfo = item.iteminfo;
            SM.mouseentered = true;
        }
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        SM.mouseentered = false;
    }

    public void ResetTreeSlot()
    {
        if (item != null)
        {
            owned = false;
            actualprice = item.price;
            SlotImage.sprite = item.icon;
            foreach (InventorySlot invslot in SM.InventorySlots)
            {
                if (invslot.occupied && invslot.item.itemname == item.itemname && !SM.TreeOccupiedCheckList.Contains(invslot))
                {
                    SlotImage.color = OwnedCol;
                    SM.TreeOccupiedCheckList.Add(invslot);
                    owned = true;
                    break;
                }
            }
            if (!owned) SlotImage.color = NotOwnedCol;

            actualprice = RecursiveCalculatePrice(item);

            SlotPriceText.text = actualprice.ToString();
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

}
