using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class OuterInventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    ShopManager SM;
    public int slotnum; //여기서는 7이 ward num

    Image ItemImage;
    Image ItemBgImage;
    TextMeshProUGUI ItemNumText;

    InventorySlot invslot;
    bool assigned = false;
    // Start is called before the first frame update
    void Start()
    {
        assigned = false;
        SM = GameObject.Find("ShopManager").GetComponent<ShopManager>();
        ItemBgImage = transform.GetChild(0).GetComponent<Image>();
        ItemImage = transform.GetChild(1).GetComponent<Image>();
        ItemNumText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!assigned)
        {
            if (SM != null && SM.InventorySlots != null)
            {
                invslot = SM.InventorySlots[slotnum];
                assigned = true;
                ChangeValue();
            }

        }
    }

    public void ChangeValue()
    {
        if (assigned && SM.StorePanel.activeInHierarchy)
        {
            ItemBgImage.enabled = invslot.ItemBgImage.enabled;
            ItemImage.sprite = invslot.ItemImage.sprite;
            ItemImage.enabled = invslot.ItemImage.enabled;
            ItemNumText.text = invslot.ItemNumText.text;
            ItemNumText.enabled = invslot.ItemNumText.enabled;
        }

    }
    private void OnEnable()
    {
        ChangeValue();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (invslot.item != null && ItemImage.enabled)
        {
            SM.MouseOnItemInfo = invslot.item.iteminfo;
            SM.mouseentered = true;
        }
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        SM.mouseentered = false;
    }
}
