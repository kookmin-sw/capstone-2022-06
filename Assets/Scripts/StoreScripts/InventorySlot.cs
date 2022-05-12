using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public ShopManager SM;
    public Item item;

    public bool iswardslot = false;
    public bool occupied = false;
    public int stackednum = 0;

    public Image ItemImage;
    public Image ItemBgImage;
    public GameObject SelectFrameObject;
    public TextMeshProUGUI ItemNumText;

    
    // Start is called before the first frame update
    void Start()
    {
        SM = GameObject.Find("ShopManager").GetComponent<ShopManager>();
        ItemBgImage = transform.GetChild(0).GetComponent<Image>();
        ItemImage = transform.GetChild(1).GetComponent<Image>();
        SelectFrameObject = transform.GetChild(2).gameObject;
        ItemNumText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SM.InventorySelect(this);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (occupied)
            {
                SM.ItemSell(this, SM.sellpercent, false);
            }
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
}
    