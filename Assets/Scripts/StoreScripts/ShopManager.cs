using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject StorePanel;
    public RectTransform CanvasRectTransform;
    bool panelactive = false;
    public int playerGold = 10000; //이후 다른 스크립트에서 static으로 선언 후 접근 해도 됨

    public bool[] elixirApplied = new bool[4]; //마법, 분노, 강철, 신속

    public Transform InventorySlotTransform;
    public InventorySlot[] InventorySlots;

    public List<GameObject> ShopItemGameobjects = new List<GameObject>();
    public Transform TabsTransform;
    TabButton[] TabButtons;
    TabButton SelectedTabButton;

    public Transform TreeSlotTransform;
    TreeSlot[] Treeslots;

    [HideInInspector]
    public InventorySlot SelectedInventorySlot;

    public Transform UpperSlotTransform;
    UpperItemSlot[] UpperSlots;

    [HideInInspector]
    public ItemInfo SelectedItemInfo;

    public Text Treetitle;
    public Text SelectedItemName;
    public Text SelectedItemPrice;
    public Text SelectedItemDescription;
    public Image SelectedItemImage;
    public Text GoldText;
    public Text SearchText;
    public Text InputFieldText;
    string previousinputfieldtext;
    public Image RevertButtonImage;
    public Image BuyButtonImage;
    
    string searchstring;
    int stackmaximum = 5;
    public float sellpercent = 1f;

    public GameObject TooltipObject;
    public Image TooltipImage;
    public Text TooltipItemNameText;
    public Text TooltipPriceText;
    public Text TooltipDescriptionText;


    public ItemInfo MouseOnItemInfo;
    [HideInInspector] public bool mouseentered;
    [HideInInspector] public float mouseenteredtime;

    [HideInInspector]
    public List<InventorySlot> TreeOccupiedCheckList = new List<InventorySlot>();

    Stack<InventorySlotRecord[]> InventorySlotRecords = new Stack<InventorySlotRecord[]>();
    Stack<int> PlayerGoldRecords = new Stack<int>();

    // Start is called before the first frame update
    void Start()
    {
        panelactive = false;
        StorePanel.SetActive(panelactive);
        Treeslots = TreeSlotTransform.GetComponentsInChildren<TreeSlot>();
        InventorySlots = InventorySlotTransform.GetComponentsInChildren<InventorySlot>();
        TabButtons = TabsTransform.GetComponentsInChildren<TabButton>();
        SelectedTabButton = TabButtons[0];
        UpperSlots = UpperSlotTransform.GetComponentsInChildren<UpperItemSlot>();

        Treeslots[0].gameObject.SetActive(false);
        Treeslots[1].gameObject.SetActive(false);
        Treeslots[2].gameObject.SetActive(false);
        Treeslots[3].gameObject.SetActive(false);

        InventorySlotRecords = new Stack<InventorySlotRecord[]>();
        PlayerGoldRecords = new Stack<int>();
        RevertButtonImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        previousinputfieldtext = "";
    }

    // Update is called once per frame
    void Update()
    {
        GoldText.text = playerGold.ToString();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            panelactive = !panelactive;
            StorePanel.SetActive(panelactive);
        }
        if (Input.GetMouseButtonDown(0)) mouseentered = false;
        if (mouseentered)
        {
            if (mouseenteredtime > 0f) mouseenteredtime -= Time.deltaTime;
            else
            {
                TooltipObject.SetActive(true);
                TooltipImage.sprite = MouseOnItemInfo.item.icon;
                TooltipItemNameText.text = MouseOnItemInfo.item.itemname;
                TooltipPriceText.text = MouseOnItemInfo.actualprice.ToString();
                TooltipDescriptionText.text = MouseOnItemInfo.item.description;
                TooltipObject.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition / CanvasRectTransform.localScale.x;
            }
        }
        else
        {
            if (mouseenteredtime < 0.5f) mouseenteredtime += Time.deltaTime * 0.2f;
            TooltipObject.SetActive(false);
        }

        if (previousinputfieldtext != InputFieldText.text.Replace(" ", ""))
        {
            previousinputfieldtext = InputFieldText.text.Replace(" ", "");
            SearchItems();
        }

    }
    public void ItemSelect(ItemInfo iteminfo)
    {
        Treetitle.text = iteminfo.name;
        SelectedItemName.text = iteminfo.name;
        SelectedItemPrice.text = iteminfo.actualprice.ToString();
        SelectedItemDescription.text = iteminfo.item.description;
        SelectedItemImage.sprite = iteminfo.item.icon;
        SelectedItemInfo = iteminfo;
        TreeSetting(iteminfo);
        UpperItemSetting(iteminfo);

        if (SelectedItemInfo.actualprice <= playerGold) BuyButtonImage.color = new Color(1f, 1f, 1f, 1f);
        else BuyButtonImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);
    }

    public void UpperItemSetting(ItemInfo iteminfo)
    {
        List<ItemInfo> UpperItemList = new List<ItemInfo>();
        foreach (GameObject g in ShopItemGameobjects)
        {
            ItemInfo searchedinfo = g.GetComponent<ItemInfo>();
            if (searchedinfo.item.loweritems.Contains(iteminfo.item))
            {
                UpperItemList.Add(searchedinfo);
            }
        }

        for (int i = 0; i < UpperSlots.Length; i++)
        {
            if (i < UpperItemList.Count)
            {
                UpperSlots[i].iteminfo = UpperItemList[i];
                UpperSlots[i].ItemImage.sprite = UpperItemList[i].item.icon;
                UpperSlots[i].gameObject.SetActive(true);
            }
            else UpperSlots[i].gameObject.SetActive(false);
        }
    }

    public void TreeSetting(ItemInfo iteminfo)
    {
        TreeOccupiedCheckList = new List<InventorySlot>();
        if (Treeslots != null)
        {
            Treeslots[0].gameObject.SetActive(true);
            TreeItemSetting(0, iteminfo.item);

            if (iteminfo.item.loweritems.Count == 0)
            {
                Treeslots[1].gameObject.SetActive(false);
                Treeslots[2].gameObject.SetActive(false);
                Treeslots[3].gameObject.SetActive(false);
            }
            else if (iteminfo.item.loweritems.Count == 1)
            {
                Treeslots[1].gameObject.SetActive(false);
                Treeslots[2].gameObject.SetActive(true);
                Treeslots[3].gameObject.SetActive(false);

                TreeItemSetting(2, iteminfo.item.loweritems[0]);

            }
            else if (iteminfo.item.loweritems.Count == 2)
            {
                Treeslots[1].gameObject.SetActive(true);
                Treeslots[2].gameObject.SetActive(false);
                Treeslots[3].gameObject.SetActive(true);

                TreeItemSetting(1, iteminfo.item.loweritems[0]);
                TreeItemSetting(3, iteminfo.item.loweritems[1]);

            }
            else if (iteminfo.item.loweritems.Count == 3)
            {
                Treeslots[1].gameObject.SetActive(true);
                Treeslots[2].gameObject.SetActive(true);
                Treeslots[3].gameObject.SetActive(true);

                TreeItemSetting(1, iteminfo.item.loweritems[0]);
                TreeItemSetting(2, iteminfo.item.loweritems[1]);
                TreeItemSetting(3, iteminfo.item.loweritems[2]);
            }
        }

    }
    public void TreeItemSetting(int n, Item item)
    {
        Treeslots[n].item = item;
        Treeslots[n].SlotImage.sprite = item.icon;
        Treeslots[n].ResetTreeSlot();
    }


    public void ItemBuy(Item item, int actualprice)
    {
        if (actualprice <= playerGold)
        {
            if (item.itemtag.Contains("stackable"))
            {
                foreach (InventorySlot invslot in InventorySlots)
                {
                    if (invslot.occupied && invslot.item.itemname == item.itemname)
                    {
                        if (invslot.stackednum >= stackmaximum) return;

                        SetInventorySlot(invslot, item, false);
                        invslot.ItemNumText.text = invslot.stackednum.ToString();
                        invslot.ItemNumText.enabled = true;
                        return;
                    }
                }  
                InventorySlot EmptySlot = FindInventoryEmptySlot();
                if (EmptySlot != null)
                {
                    SetInventorySlot(EmptySlot, item, false);
                    EmptySlot.ItemNumText.text = EmptySlot.stackednum.ToString();
                    EmptySlot.ItemNumText.enabled = true;

                }
            }
            else if (item.itemtag.Contains("ward"))
            {
                InventorySlots[6].ItemNumText.enabled = false;
                SetInventorySlot(InventorySlots[6], item, false);

            }
            else if (item.itemtag.Contains("elixir"))
            {
                if (item.itemname == "마법의 영약") elixirApplied[0] = true;
                else if (item.itemname == "분노의 영약") elixirApplied[1] = true;
                else if(item.itemname == "강철의 영약") elixirApplied[2] = true;
                else if(item.itemname == "신속의 영약") elixirApplied[3] = true;

                InventorySlotRecords.Clear(); //영약을 먹으면 되돌리기 할 수 없음
                PlayerGoldRecords.Clear();
                RevertButtonImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                playerGold -= item.price;
            }
            else if (item.itemtag.Contains("shoes"))
            {
                foreach (InventorySlot invslot in InventorySlots)
                {
                    if (invslot.occupied && invslot.item.itemtag.Contains("shoes"))
                    {
                        if (item.itemtag.Contains("base shoes")) return;
                        if (item.itemtag.Contains("advanced shoes") && invslot.item.itemtag.Contains("advanced shoes")) return;
                    }
                }
                RecursiveItemSeller(item);
                InventorySlot EmptySlot = FindInventoryEmptySlot();
                if (EmptySlot != null)
                {
                    EmptySlot.ItemNumText.enabled = false;
                    SetInventorySlot(EmptySlot, item, true);
                }
            }
            else
            {
                RecursiveItemSeller(item);
                InventorySlot EmptySlot = FindInventoryEmptySlot();
                if (EmptySlot != null)
                {
                    EmptySlot.ItemNumText.enabled = false;
                    SetInventorySlot(EmptySlot, item, true);
                }
            }
        }
    }

    void RecursiveItemSeller(Item item)
    {
        RecordInventory();
        RecursiveItemSellerHelper(item);
    }
    void RecursiveItemSellerHelper(Item item)
    {
        if (item.loweritems.Count > 0)
        {
            foreach (Item loweritem in item.loweritems)
            {
                bool exist = false;
                foreach (InventorySlot invslot in InventorySlots)
                {
                    if (invslot.occupied && loweritem.itemname == invslot.item.itemname)
                    {
                        ItemSell(invslot, 1f, true);
                        exist = true;
                        break;
                    }
                }
                if (!exist) RecursiveItemSellerHelper(loweritem);
            }
        }
    }


    public void BuyButtonItemBuy()
    {
        if (SelectedItemInfo != null) ItemBuy(SelectedItemInfo.item, SelectedItemInfo.actualprice);
    }
    public void SellButtonItemSell()
    {
        if (SelectedInventorySlot != null)
        {
            ItemSell(SelectedInventorySlot, sellpercent, false);
        }
    }
    public void ItemSell(InventorySlot invslot, float percent, bool isrecursivesell) //판매할 때 받는 돈 퍼센트. 기본 1 (100%)
    {
        if (invslot.stackednum > 1)
        {
            if (!isrecursivesell) RecordInventory();
            invslot.stackednum -= 1;
            playerGold += Mathf.FloorToInt(invslot.item.price * percent);
            invslot.ItemNumText.text = invslot.stackednum.ToString();
            InventorySelect(invslot);
            CalculateActualPriceAll();
            return;
        }
        else
        {
            if (!isrecursivesell) RecordInventory();
            invslot.stackednum = 0;
            playerGold += Mathf.FloorToInt(invslot.item.price * percent);
            invslot.ItemImage.enabled = false;
            invslot.ItemBgImage.enabled = false;
            invslot.occupied = false;
            invslot.ItemNumText.text = invslot.stackednum.ToString();
            invslot.ItemNumText.enabled = false;

            SelectedInventorySlot = null;
            foreach (InventorySlot slot in InventorySlots)
            {
                slot.SelectFrameObject.SetActive(false);
            }
            CalculateActualPriceAll();
            return;
        }

    }



    InventorySlot FindInventoryEmptySlot()
    {
        foreach (InventorySlot invslot in InventorySlots)
        {
            if (!invslot.occupied && !invslot.iswardslot) return invslot;
        }
        return null;
    }
    void SetInventorySlot(InventorySlot invslot, Item item, bool isrecursivebuy)
    {
        if (!isrecursivebuy) RecordInventory();
        invslot.occupied = true;
        playerGold -= item.price;
        invslot.item = item;

        invslot.ItemImage.sprite = invslot.item.icon;
        invslot.ItemImage.enabled = true;
        invslot.ItemBgImage.enabled = true;

        invslot.stackednum += 1;
        GoldText.text = playerGold.ToString();
        CalculateActualPriceAll();
    }

    public void InventorySelect(InventorySlot invslot)
    {
        SelectedInventorySlot = null;
        foreach (InventorySlot slot in InventorySlots)
        {
            slot.SelectFrameObject.SetActive(false);
            if (slot == invslot && invslot.occupied)
            {
                slot.SelectFrameObject.SetActive(true);
                SelectedInventorySlot = invslot;
            }
        }
    }

    public void TabSelect(TabButton tabbuton)
    {
        int? tabid = null;
        for (int i = 0; i < TabButtons.Length; i++)
        {
            if (TabButtons[i] == tabbuton)
            {
                tabid = i;
                TabButtons[i].Selected();
                SelectedTabButton = TabButtons[i];
            }
            else
            {
                TabButtons[i].DeSelected();
            }
        }
        if (tabid != null)
        {
            if (tabid == 0) //ALL
            {
                TabActiveItems("all");

            }
            else if (tabid == 1)
            {
                TabActiveItems("comsumables");
            }
            else if (tabid == 2)
            {
                TabActiveItems("basic");
            }
            else if (tabid == 3)
            {
                TabActiveItems("advanced");
            }
            else if (tabid == 4)
            {
                TabActiveItems("final");
            }
            else if (tabid == 5)
            {
                TabActiveItems("health");
            }
            else if (tabid == 6)
            {
                TabActiveItems("mana");
            }
            else if (tabid == 7)
            {
                TabActiveItems("attack damage");
            }
            else if (tabid == 8)
            {
                TabActiveItems("ability power");
            }
            else if (tabid == 9)
            {
                TabActiveItems("defense");
            }
            else if (tabid == 10)
            {
                TabActiveItems("cooldown reduction");
            }
            else if (tabid == 11)
            {
                TabActiveItems("health regeneration");
            }
            else if (tabid == 12)
            {
                TabActiveItems("mana regeneration");
            }
            else if (tabid == 13)
            {
                TabActiveItems("attack speed");
            }
            else if (tabid == 14)
            {
                TabActiveItems("move speed");
            }

        }

    }

    void RecordInventory()
    {
        InventorySlotRecord[] SaveInventorySlots = new InventorySlotRecord[7];

        for (int i = 0; i < 7; i++)
        {
            InventorySlotRecord saveslot = new InventorySlotRecord();
            saveslot.item = InventorySlots[i].item;
            saveslot.iswardslot = InventorySlots[i].iswardslot;
            saveslot.occupied = InventorySlots[i].occupied;
            saveslot.stackednum = InventorySlots[i].stackednum;
            SaveInventorySlots[i] = saveslot;
        }
        InventorySlotRecords.Push(SaveInventorySlots);
        PlayerGoldRecords.Push(playerGold);

    }

    void TabActiveItems(string tag)
    {
        if (tag == "all")
        {
            foreach (GameObject itemobject in ShopItemGameobjects)
            {
                if (searchstring != null && searchstring != "")
                {
                    if (itemobject.GetComponent<ItemInfo>().item.itemname.Replace(" ", "").Contains(searchstring))
                    {
                        itemobject.SetActive(true);
                    }
                    else
                    {
                        itemobject.SetActive(false);
                    }
                }
                else
                {
                    itemobject.SetActive(true);
                }
            }
            return;
        }
        foreach (GameObject itemobject in ShopItemGameobjects)
        {
            if (itemobject.GetComponent<ItemInfo>().item.itemtag.Contains(tag))
            {
                if (searchstring != null && searchstring != "")
                {
                    if (itemobject.GetComponent<ItemInfo>().item.itemname.Replace(" ", "").Contains(searchstring))
                    {
                        itemobject.SetActive(true);
                    }
                    else
                    {
                        itemobject.SetActive(false);
                    }
                }
                else
                {
                    itemobject.SetActive(true);
                }
                
            }
            else
            {
                itemobject.SetActive(false);
            }
        }
    }

    public void CalculateActualPriceAll()
    {
        TreeOccupiedCheckList = new List<InventorySlot>();
        foreach (TreeSlot treeslot in Treeslots)
        {
            treeslot.ResetTreeSlot();
        }
        foreach (GameObject itemgameobject in ShopItemGameobjects)
        {
            itemgameobject.GetComponent<ItemInfo>().CalculateActualPrice();
        }

        SelectedItemPrice.text = SelectedItemInfo.actualprice.ToString();
        if (InventorySlotRecords.Count > 0) RevertButtonImage.color = new Color(1f, 1f, 1f, 1f);
        else RevertButtonImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);

        if (SelectedItemInfo.actualprice <= playerGold) BuyButtonImage.color = new Color(1f, 1f, 1f, 1f);
        else BuyButtonImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);
    }


    public void RevertButton()
    {
        if (InventorySlotRecords.Count > 0)
        {
            int i = 0;
            playerGold = PlayerGoldRecords.Pop();
            GoldText.text = playerGold.ToString();

            SelectedInventorySlot = null;
            foreach (InventorySlotRecord savedslot in InventorySlotRecords.Pop())
            {
                InventorySlots[i].item = savedslot.item;
                InventorySlots[i].iswardslot = savedslot.iswardslot;
                InventorySlots[i].occupied = savedslot.occupied;
                InventorySlots[i].stackednum = savedslot.stackednum;
                if (InventorySlots[i].occupied && InventorySlots[i].item.itemtag.Contains("stackable"))
                {
                    InventorySlots[i].ItemNumText.text = InventorySlots[i].stackednum.ToString();
                    InventorySlots[i].ItemNumText.enabled = true;
                }
                else InventorySlots[i].ItemNumText.enabled = false;

                if (InventorySlots[i].occupied) InventorySlots[i].ItemImage.sprite = InventorySlots[i].item.icon;
                InventorySlots[i].ItemImage.enabled = InventorySlots[i].occupied;
                InventorySlots[i].ItemBgImage.enabled = InventorySlots[i].occupied;
                InventorySlots[i].SelectFrameObject.SetActive(false);
                i++;
            }
        }
        CalculateActualPriceAll();
    }
    
    public void SearchItems()
    {
        searchstring = InputFieldText.text.Replace(" ","");
        TabSelect(SelectedTabButton);
    }
}
