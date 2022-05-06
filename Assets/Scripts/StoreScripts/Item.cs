using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string itemname;
    public string description;
    public int price;
    public Sprite icon;
    public List<string> itemtag;
    public Dictionary<string, int> stats = new Dictionary<string, int>();
    public List<string> loweritemnames = new List<string>();
    public List<Item> loweritems = new List<Item>();

    public ItemInfo iteminfo;

    public Item(string itemname, string description, int price,
                Sprite icon, List<string> itemtag, Dictionary<string, int> stats, List<string> loweritemnames, List<Item> loweritems)
    {
        this.itemname = itemname;
        this.description = description;
        this.price = price;
        this.icon = icon;
        this.itemtag = itemtag;
        this.stats = stats;
        this.loweritemnames = loweritemnames;
        this.loweritems = loweritems;
    }
}
