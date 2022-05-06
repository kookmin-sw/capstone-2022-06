using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public List<Item> ItemList = new List<Item>();
    public GameObject ItemPrefab;
    public Transform ItemSlot;
    ShopManager SM;


    public Sprite LoadSprite(string spritename)
    {
        return Resources.Load<Sprite>("Textures/Items/" + spritename);
    }
    // Start is called before the first frame update
    void Start()
    {
        SM = GameObject.Find("ShopManager").GetComponent<ShopManager>();
        CreateShopItems();
        SetLowerItems();
        CreatePrefabs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CreateShopItems()
    {
        ItemList = new List<Item>();
        ItemList.AddRange(PotionWardItems());
        ItemList.AddRange(BasicItems());
        ItemList.AddRange(AdvancedItems());
        ItemList.AddRange(FinalItems());
        ItemList.AddRange(ShoesItems());
    }

    List<Item> PotionWardItems()
    {
        return new List<Item>()
        {
            new Item(
                "ü�� ����",        //������ �̸�
                "10�� ���� HP +150 ȸ��", //������ ����
                50,                 //������ ����
                LoadSprite("hp"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "potion", "comsumables", "stackable"
                },
                new Dictionary<string, int>(){ //������ ���� (���� ���� �����Ͻ� �� Ȱ���Ͻø� �˴ϴ�!)
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "�� ū ü�� ����", //������ �̸�
                "10�� ���� HP +1000 ȸ��", //������ ����
                250, //������ ����
                LoadSprite("Elixir_1"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "potion", "comsumables", "stackable"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "���� ����", //������ �̸�
                "10�� ���� MP +50 ȸ��", //������ ����
                100, //������ ����
                LoadSprite("mp"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "potion", "comsumables", "stackable"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "�� ū ���� ����", //������ �̸�
                "10�� ���� MP +200 ȸ��", //������ ����
                300, //������ ����
                LoadSprite("Elixir_2"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "potion", "comsumables", "stackable"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "�͵� ����", //������ �̸�
                "�Ʊ� ��ο��� 90~120�� ���� �ֺ� ������ ���� �ִ� ���� �͵� �ϳ��� ���鿡 ��ġ�մϴ�.", //������ ����
                0, //������ ����
                LoadSprite("Arcanist15"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "ward",  "comsumables"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "������ ��", //������ �̸�
                "��ó�� �͵������� �� �� �ֽ��ϴ�.", //������ ����
                0, //������ ����
                LoadSprite("Stone_1"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "ward", "comsumables"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "������ ����", //������ �̸�
                "3�� ���� �ֹ��� +10", //������ ����
                500, //������ ����
                LoadSprite("Elixir_4"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "elixir", "comsumables"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "�г��� ����", //������ �̸�
                "3�� ���� ���ݷ� +10", //������ ����
                500, //������ ����
                LoadSprite("Elixir_6"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "elixir", "comsumables"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "��ö�� ����", //������ �̸�
                "3�� ���� ���� +10", //������ ����
                500, //������ ����
                LoadSprite("Elixir_5"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "elixir", "comsumables"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "�ż��� ����", //������ �̸�
                "3�� ���� �̵��ӵ� + 10", //������ ����
                500, //������ ����
                LoadSprite("Elixir_3"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "elixir", "comsumables"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            )
        };
    }
    List<Item> BasicItems()
    {
        return new List<Item>()
        {
            new Item(
                "������ ����",        //������ �̸�
                "�ֹ��� +15, ü�� +70", //������ ����
                450,                 //������ ����
                LoadSprite("rings"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "basic"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "������ ��",        //������ �̸�
                "���ݷ� +5, ü�� +100", //������ ����
                450,                 //������ ����
                LoadSprite("sword"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "basic"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "������ ����",        //������ �̸�
                "���� +5, ü�� +100", //������ ����
                450,                 //������ ����
                LoadSprite("shield_2"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "basic"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "õ ����",        //������ �̸�
                "���� +15", //������ ����
                300,                 //������ ����
                LoadSprite("armor"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "basic", "defense"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "ȸ���� �۷κ�",        //������ �̸�
                "�⺻ ü�� ��� +100%", //������ ����
                300,                 //������ ����
                LoadSprite("04_cloth_gloves_E"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "basic", "health regeneration"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "�ܴ��� �ϼ�",        //������ �̸�
                "ü�� +150", //������ ����
                400,                 //������ ����
                LoadSprite("02_Iron_Ore"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "basic", "health"
                },
                new Dictionary<string, int>(){ //������ ���� (���� ���� �����Ͻ� �� Ȱ���Ͻø� �˴ϴ�!)
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "�ż��� ��",        //������ �̸�
                "���� �ӵ� +12% ", //������ ����
                300,                 //������ ����
                LoadSprite("02_Sword"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "basic", "attack speed"
                },
                new Dictionary<string, int>(){ //������ ���� (���� ���� �����Ͻ� �� Ȱ���Ͻø� �˴ϴ�!)
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "�յ���",        //������ �̸�
                "���ݷ� +10", //������ ����
                350,                 //������ ����
                LoadSprite("06_One_Handed_Axe"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "basic", "attack damage"
                },
                new Dictionary<string, int>(){ //������ ���� (���� ���� �����Ͻ� �� Ȱ���Ͻø� �˴ϴ�!)
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "��׽�Ÿ",        //������ �̸�
                "���ݷ� +25", //������ ����
                875,                 //������ ����
                LoadSprite("04_Mace_with_spikes"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "basic", "attack damage"
                },
                new Dictionary<string, int>(){ //������ ���� (���� ���� �����Ͻ� �� Ȱ���Ͻø� �˴ϴ�!)
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "������ �����",        //������ �̸�
                "��Ÿ�� ���� +5%, �̵� �ӵ� +5%", //������ ����
                300,                 //������ ����
                LoadSprite("necklace"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "basic", "cooldown reduction", "move speed"
                },
                new Dictionary<string, int>(){ //������ ���� (���� ���� �����Ͻ� �� Ȱ���Ͻø� �˴ϴ�!)
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "�����̾� ����",        //������ �̸�
                "���� +250", //������ ����
                350,                 //������ ����
                LoadSprite("gem"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "basic", "mana"
                },
                new Dictionary<string, int>(){ //������ ���� (���� ���� �����Ͻ� �� Ȱ���Ͻø� �˴ϴ�!)
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "������ ����",        //������ �̸�
                "�⺻ ���� ��� +50%", //������ ����
                250,                 //������ ����
                LoadSprite("scroll"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "basic", "mana regeneration"
                },
                new Dictionary<string, int>(){ //������ ���� (���� ���� �����Ͻ� �� Ȱ���Ͻø� �˴ϴ�!)
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "���������� ��",        //������ �̸�
                "�ֹ��� +20", //������ ����
                435,                 //������ ����
                LoadSprite("book"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "basic", "ability power"
                },
                new Dictionary<string, int>(){ //������ ���� (���� ���� �����Ͻ� �� Ȱ���Ͻø� �˴ϴ�!)
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            ),
            new Item(
                "���� ������",        //������ �̸�
                "�ֹ��� +40", //������ ����
                850,                 //������ ����
                LoadSprite("08_Mage_Staff"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "basic", "ability power"
                },
                new Dictionary<string, int>(){ //������ ���� (���� ���� �����Ͻ� �� Ȱ���Ͻø� �˴ϴ�!)
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {

                },
                new List<Item>()
            )
        };
    }
    List<Item> AdvancedItems()
    {
        return new List<Item>()
        {
            new Item(
                "�ֹ������� ����",        //������ �̸�
                "���ݷ� +25,���� +35", //������ ����
                1300,                 //������ ����
                LoadSprite("10_Basic_Leather"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "attack damage", "defense"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {
                    "��׽�Ÿ", "õ ����"
                },
                new List<Item>()
            ),
            new Item(
                "������ ����",        //������ �̸�
                "ü�� +350", //������ ����
                900,                 //������ ����
                LoadSprite("03_Leather_helm_E"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "health"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {
                    "�ܴ��� �ϼ�", "�ܴ��� �ϼ�"
                },
                new List<Item>()
            ),
            new Item(
                "Ž���� �յ���",        //������ �̸�
                "���ݷ� +15, ü�� +200", //������ ����
                1100,                 //������ ����
                LoadSprite("03_Greataxe"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "health", "attack damage"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {
                    "�ܴ��� �ϼ�", "�յ���"
                },
                new List<Item>()
            ),
            new Item(
                "�Ǹ��� ������",        //������ �̸�
                "�ֹ��� +25, ��Ÿ�� ���� +10%", //������ ����
                900,                 //������ ����
                LoadSprite("Enchanter18"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "ability power", "cooldown reduction"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {
                    "���������� ��", "������ �����"
                },
                new List<Item>()
            )
        };
    }
    List<Item> FinalItems()
    {
        return new List<Item>()
        {
            new Item(
                "�ݺ� ����",        //������ �̸�
                "���ݷ� +40, ü�� +300, ���� +30 ", //������ ����
                3000,                 //������ ����
                LoadSprite("Ax_2"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "final", "defense", "health", "attack damage"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {
                    "�ֹ������� ����", "������ ����"
                },
                new List<Item>()
            ),
            new Item(
                "������ ����",        //������ �̸�
                "���ݷ� +45, ü�� +250, ��Ÿ�� ���� +10%", //������ ����
                2600,                 //������ ����
                LoadSprite("Helmet_4"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "final", "cooldown reduction", "health", "attack damage"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {
                    "Ž���� �յ���", "�Ǹ��� ������"
                },
                new List<Item>()
            )
        };
    }
    List<Item> ShoesItems()
    {
        return new List<Item>()
        {
            new Item(
                "�Ź�",        //������ �̸�
                "�̵� �ӵ� +25", //������ ����
                300,                 //������ ����
                LoadSprite("05_leather_boots"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "shoes", "move speed", "base shoes"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {
                    
                },
                new List<Item>()
            ),
            new Item(
                "�ż��� �Ź�",        //������ �̸�
                "�̵� �ӵ� + 60", //������ ����
                900,                 //������ ����
                LoadSprite("05_cloth_boots_E"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "shoes", "move speed", "advanced shoes"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {
                    "�Ź�"
                },
                new List<Item>()
            ),
            new Item(
                "������ �Ź�",        //������ �̸�
                "�̵� �ӵ� + 45, ��ų ��Ÿ�� ���� +10%", //������ ����
                950,                 //������ ����
                LoadSprite("05_leather_boots_E"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "shoes", "move speed", "advanced shoes"
                },
                new Dictionary<string, int>(){ //������ ����
                    //{"str", 1},
                },
                new List<string>() //���� ������ �̸�
                {
                    "�Ź�"
                },
                new List<Item>()
            )
        };
    }

    void CreatePrefabs()
    {
        foreach (Item item in ItemList)
        {
            GameObject pref = Instantiate(ItemPrefab, ItemSlot);
            pref.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
            pref.transform.GetChild(1).GetComponent<Text>().text = item.price.ToString();
            pref.name = item.itemname;
            
            pref.GetComponent<ItemInfo>().item = item;
            item.iteminfo = pref.GetComponent<ItemInfo>();
            SM.ShopItemGameobjects.Add(pref);
        }
    }
    void SetLowerItems()
    {
        foreach (Item item in ItemList)
        {
            if (item.loweritemnames.Count > 0)
            {
                item.loweritems = new List<Item>();
                for (int i = 0; i < item.loweritemnames.Count; i++)
                {
                    foreach (Item item_1 in ItemList)
                    {
                        if (item.loweritemnames[i] == item_1.itemname) 
                        {
                            item.loweritems.Add(item_1);
                            break;
                        }
                    }
                }
            }
        }
    }
}
