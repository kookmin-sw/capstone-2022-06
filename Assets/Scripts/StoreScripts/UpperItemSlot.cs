using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpperItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    ShopManager SM;
    public ItemInfo iteminfo;
    public Image ItemImage;
    // Start is called before the first frame update
    void Awake()
    {
        SM = GameObject.Find("ShopManager").GetComponent<ShopManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left || eventData.button == PointerEventData.InputButton.Right)
        {
            SM.ItemSelect(iteminfo);
        }
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (iteminfo != null)
        {
            SM.MouseOnItemInfo = iteminfo;
            SM.mouseentered = true;
        }
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        SM.mouseentered = false;
    }
}
