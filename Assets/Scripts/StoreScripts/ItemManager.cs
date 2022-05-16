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
        return Resources.Load<Sprite>("Private/Textures/Items/" + spritename);
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
                new Dictionary<string, int>(){ //������ ����
                    {"hp", 150}
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
                    {"hp", 1000}
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
                    {"mp", 50}
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
                    {"mp", 200}
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
                    //{"", }
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
                    //{"", }
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
                    {"ap", 10}
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
                    {"atk", 10}
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
                    {"defense", 10}
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
                    {"moveSpeed", 10}
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
                    {"ap", 15},
                    {"hp", 70}
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
                    {"atk", 5},
                    {"hp", 100}
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
                    {"defense", 5},
                    {"hp", 100}
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
                    {"defense", 15}
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
                    {"healthRegen", 100}
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
                new Dictionary<string, int>(){ //������ ���� 
                    {"hp", 150}
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
                new Dictionary<string, int>(){ //������ ����
                    {"atkSpeed", 12}
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
                new Dictionary<string, int>(){ //������ ���� 
                    {"atk", 10}
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
                new Dictionary<string, int>(){ //������ ����
                    {"atk", 25}
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
                new Dictionary<string, int>(){ //������ ����
                    {"coolDown", 5},
                    {"moveSpeed", 5}
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
                new Dictionary<string, int>(){ //������ ���� 
                    {"mp", 250}
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
                new Dictionary<string, int>(){ //������ ����
                    {"manaRegen", 50}
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
                new Dictionary<string, int>(){ //������ ���� 
                    {"ap", 20}
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
                new Dictionary<string, int>(){ //������ ����
                    {"ap", 40}
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
        {   new Item(
                "Ǫ���� ȯ��",        //������ �̸�
                "���ݷ� +40, �̵� �ӵ� +5%", //������ ����
                1400,                 //������ ����
                LoadSprite("08_Blink"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "attack damage", "move speed"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"atk", 40},
                    {"moveSpeed", 5}
                },
                new List<string>() //���� ������ �̸�
                {
                    "���� ������", "������ �����"
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
                    {"ap", 25},
                    {"coolDown", 10}
                },
                new List<string>() //���� ������ �̸�
                {
                    "���������� ��", "������ �����"
                },
                new List<Item>()
            ),
            new Item(
                "������ ��",        //������ �̸�
                "�ֹ��� +40, ���� +300, ��Ÿ�� ���� +5%", //������ ����
                1300,                 //������ ����
                LoadSprite("06_blue_flower"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "ability power", "mana", "cooldown reduction"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"ap", 40},
                    {"mp", 300},
                    {"coolDown", 5}
                },
                new List<string>() //���� ������ �̸�
                {
                    "���� ������", "�����̾� ����"
                },
                new List<Item>()
            ),
            new Item(
                "����� ����",        //������ �̸�
                "�ֹ��� +20, ü�� +250", //������ ����
                1300,                 //������ ����
                LoadSprite("01_cloth_chest"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "ability power", "health"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"ap", 20},
                    {"hp", 250}
                },
                new List<string>() //���� ������ �̸�
                {
                    "���������� ��", "�ܴ��� �ϼ�"
                },
                new List<Item>()
            ),
            new Item(
                "�ŷ��� ����",        //������ �̸�
                "�ֹ��� +20, ���� +25", //������ ����
                1000,                 //������ ����
                LoadSprite("01_plate_chest_E"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "ability power", "defense"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"ap", 20},
                    {"defense", 25}
                },
                new List<string>() //���� ������ �̸�
                {
                    "���������� ��", "õ ����"
                },
                new List<Item>()
            ),
            new Item(
                "������ ��",        //������ �̸�
                "�ֹ��� +25, ���� �ӵ� +20%", //������ ����
                1250,                 //������ ����
                LoadSprite("05_red_flower"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "ability power", "attack speed"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"ap", 25},
                    {"atkSpeed", 20}
                },
                new List<string>() //���� ������ �̸�
                {
                    "���������� ��", "�ż��� ��"
                },
                new List<Item>()
            ),
            new Item(
                "������ Ȱ",        //������ �̸�
                "�ֹ��� +30, ���� �ӵ� +15%", //������ ����
                1300,                 //������ ����
                LoadSprite("01_Bow"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "ability power", "attack speed"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"ap", 30},
                    {"atkSpeed", 15}
                },
                new List<string>() //���� ������ �̸�
                {
                    "��׽�Ÿ", "�ż��� ��"
                },
                new List<Item>()
            ),
            new Item(
                "������ ��",        //������ �̸�
                "���� �ӵ� +18%, �̵� �ӵ� +7%", //������ ����
                1050,                 //������ ����
                LoadSprite("09_Longsword"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "move speed", "attack speed"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"atkSpeed", 18},
                    {"moveSpeed", 7}
                },
                new List<string>() //���� ������ �̸�
                {
                    "�յ���", "������ �����"
                },
                new List<Item>()
            ),
            new Item(
                "�ʵ��� ����",        //������ �̸�
                "���ݷ� +25, ��Ÿ�� ���� +10%", //������ ����
                1250,                 //������ ����
                LoadSprite("player_portrait_avatar_example"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "attack damage", "cooldown reduction"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"atk", 25},
                    {"coolDown", 10}
                },
                new List<string>() //���� ������ �̸�
                {
                    "��׽�Ÿ", "������ �����"
                },
                new List<Item>()
            ),
            new Item(
                "�ֹ������� ����",        //������ �̸�
                "���ݷ� +25, ���� +35", //������ ����
                1300,                 //������ ����
                LoadSprite("10_Basic_Leather"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "attack damage", "defense"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"atk", 25},
                    {"defense", 35}
                },
                new List<string>() //���� ������ �̸�
                {
                    "��׽�Ÿ", "õ ����"
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
                    {"atk", 15},
                    {"hp", 200}
                },
                new List<string>() //���� ������ �̸�
                {
                    "�ܴ��� �ϼ�", "�յ���"
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
                    {"hp", 350},
                },
                new List<string>() //���� ������ �̸�
                {
                    "�ܴ��� �ϼ�", "�ܴ��� �ϼ�"
                },
                new List<Item>()
            ),
            new Item(
                "������ �尩",        //������ �̸�
                "ü�� +250, ���� +25", //������ ����
                1250,                 //������ ����
                LoadSprite("04_plate_gloves_E"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "health", "defense"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"hp", 250},
                    {"defense", 25}
                },
                new List<string>() //���� ������ �̸�
                {
                    "�ܴ��� �ϼ�", "õ ����"
                },
                new List<Item>()
            ),
            new Item(
                "������ ����",        //������ �̸�
                "ü�� +200, ��Ÿ�� ���� +10%", //������ ����
                800,                 //������ ����
                LoadSprite("05_Shield"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "health", "cooldown reduction"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"hp", 200},
                    {"coolDown", 10}
                },
                new List<string>() //���� ������ �̸�
                {
                    "�ܴ��� �ϼ�", "������ �����"
                },
                new List<Item>()
            ),
            new Item(
                "����� ö��",        //������ �̸�
                "ü�� +150, �̵� �ӵ� +5%", //������ ����
                800,                 //������ ����
                LoadSprite("01_plate_chest"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "health", "move speed"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"hp", 150},
                    {"moveSpeed", 5}
                },
                new List<string>() //���� ������ �̸�
                {
                    "�ܴ��� �ϼ�", "������ �����"
                },
                new List<Item>()
            ),
            new Item(
                "��������",        //������ �̸�
                "���� +20, ���� +250, ��Ÿ�� ���� +10%", //������ ����
                900,                 //������ ����
                LoadSprite("Helmet_3"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "defense", "mana", "cooldown reduction"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"defense", 20},
                    {"mp", 250},
                    {"coolDown", 10}
                },
                new List<string>() //���� ������ �̸�
                {
                    "õ ����", "�����̾� ����"
                },
                new List<Item>()
            ),
            new Item(
                "���� �߰�",        //������ �̸�
                "���� +50", //������ ����
                1000,                 //������ ����
                LoadSprite("02_plate_shoulder_E"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "defense"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"defense", 50}
                },
                new List<string>() //���� ������ �̸�
                {
                    "õ ����", "õ ����"
                },
                new List<Item>()
            ),
            new Item(
                "������ �κ�",        //������ �̸�
                "ü�� +150, ���� +30, �⺻ ü�� ��� +100%", //������ ����
                1400,                 //������ ����
                LoadSprite("03_Cloth_helm_E"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "defense", "health", "health regeneration"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"hp", 150},
                    {"defense", 30},
                    {"healthRegen", 100}
                },
                new List<string>() //���� ������ �̸�
                {
                    "õ ����", "ȸ���� �۷κ�"
                },
                new List<Item>()
            ),
            new Item(
                "������ ����",        //������ �̸�
                "�⺻ ü�� ��� +100%, �⺻ ���� ��� +50%", //������ ����
                800,                 //������ ����
                LoadSprite("Ring_2"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "advanced", "health regeneration", "mana regeneration"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"healthRegen", 100},
                    {"manaRegen", 50}
                },
                new List<string>() //���� ������ �̸�
                {
                    "ȸ���� �۷κ�", "������ ����"
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
                    {"atk", 40},
                    {"hp", 300},
                    {"defense", 30}
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
                    {"atk", 45},
                    {"hp", 250},
                    {"coolDown", 10}
                },
                new List<string>() //���� ������ �̸�
                {
                    "Ž���� �յ���", "�Ǹ��� ������"
                },
                new List<Item>()
            ),
            new Item(
                "Ǫ�� ������",        //������ �̸�
                "���ݷ� +50, ü�� +400, �⺻ ü�� ��� +150%", //������ ����
                2800,                 //������ ����
                LoadSprite("Shurikens"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "final", "health regeneration", "health", "attack damage"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"atk", 50},
                    {"hp", 400},
                    {"healthRegen", 150}
                },
                new List<string>() //���� ������ �̸�
                {
                    "Ž���� �յ���", "�ŷ��� ����"
                },
                new List<Item>()
            ),
            new Item(
                "�����ҵ�",        //������ �̸�
                "���ݷ� +35, ���� +500, ��Ÿ�� ���� +15%", //������ ����
                2900,                 //������ ����
                LoadSprite("Sword_1"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "final", "cooldown reduction", "mana", "attack damage"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"atk", 35},
                    {"mana", 500},
                    {"coolDown", 15}
                },
                new List<string>() //���� ������ �̸�
                {
                    "�ֹ������� ����", "������ ��"
                },
                new List<Item>()
            ),
            new Item(
                "�������� �ܰ�",        //������ �̸�
                "���ݷ� +40, ���� �ӵ� +40%, ���� +30", //������ ����
                3100,                 //������ ����
                LoadSprite("Sword_2"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "final", "attack speed", "defense", "attack damage"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"atk", 40},
                    {"atkSpeed", 40},
                    {"defense", 30}
                },
                new List<string>() //���� ������ �̸�
                {
                    "Ž���� �յ���", "������ Ȱ"
                },
                new List<Item>()
            ),
            new Item(
                "���ָ� ����",        //������ �̸�
                "���ݷ� +70, ��Ÿ�� ���� +10%", //������ ����
                3300,                 //������ ����
                LoadSprite("Ax_1"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "final", "cooldown reduction", "attack damage"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"atk", 70},
                    {"coolDown", 10}
                },
                new List<string>() //���� ������ �̸�
                {
                    "�ʵ��� ����", "������ Ȱ"
                },
                new List<Item>()
            ),
            new Item(
                "������ ���",        //������ �̸�
                "�ֹ��� +100, ���� �ӵ� +50%", //������ ����
                3000,                 //������ ����
                LoadSprite("Bow"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "final", "ability power", "attack speed"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"ap", 100},
                    {"atkSpeed", 50}
                },
                new List<string>() //���� ������ �̸�
                {
                    "������ ��", "Ǫ���� ȯ��"
                },
                new List<Item>()
            ),
            new Item(
                "������ �Ҵ�Ʈ",        //������ �̸�
                "�ֹ��� +75, ü�� +400, �⺻ ���� ��� +100%", //������ ����
                2600,                 //������ ����
                LoadSprite("Amulet_1"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "final", "ability power", "health", "mana regeneration"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"ap", 75},
                    {"hp", 400},
                    {"manaRegen0", 100}
                },
                new List<string>() //���� ������ �̸�
                {
                    "������ ��", "������ ����"
                },
                new List<Item>()
            ),
            new Item(
                "������ ����",        //������ �̸�
                "ü�� +300, ���� +45, �̵� �ӵ� +5%", //������ ����
                2900,                 //������ ����
                LoadSprite("shield"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "final", "defense", "health", "move speed"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"hp", 300},
                    {"defense", 45},
                    {"moveSpeed", 5}
                },
                new List<string>() //���� ������ �̸�
                {
                    "������ ����", "����� ö��"
                },
                new List<Item>()
            ),
            new Item(
                "��ö�� ����",        //������ �̸�
                "ü�� +800, ü�� ��� +200%, ���� +30", //������ ����
                3500,                 //������ ����
                LoadSprite("Armor_2"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "final", "health regeneration", "health", "defense"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"hp", 800},
                    {"healthRegen", 200},
                    {"defense", 30}
                },
                new List<string>() //���� ������ �̸�
                {
                    "������ ����", "������ �κ�"
                },
                new List<Item>()
            ),
            new Item(
                "��õ���� ����",        //������ �̸�
                "�ֹ��� +60, ü�� +200, ���� +500", //������ ����
                2600,                 //������ ����
                LoadSprite("Ring_1"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "final", "ability power", "health", "mana"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"ap", 60},
                    {"hp", 200},
                    {"mana", 500}
                },
                new List<string>() //���� ������ �̸�
                {
                    "���� ������", "����� ����"
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
                    {"moveSpeed", 25}
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
                    {"moveSpeed", 60}
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
                    {"moveSpeed", 45},
                    {"coolDown", 10}
                },
                new List<string>() //���� ������ �̸�
                {
                    "�Ź�"
                },
                new List<Item>()
            ),
            new Item(
                "�������� �Ź�",        //������ �̸�
                "�̵� �ӵ� +45, ���� �ӵ� +30%", //������ ����
                1100,                 //������ ����
                LoadSprite("05_plate_boots_E"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "shoes", "move speed", "advanced shoes"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"moveSpeed", 45},
                    {"atkSpeed", 30}
                },
                new List<string>() //���� ������ �̸�
                {
                    "�Ź�"
                },
                new List<Item>()
            ),
            new Item(
                "�������� �Ź�",        //������ �̸�
                "�̵� �ӵ� +45, �ֹ��� +15", //������ ����
                1100,                 //������ ����
                LoadSprite("05_cloth_boots"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "shoes", "move speed", "advanced shoes"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"moveSpeed", 45},
                    {"ap", 15}
                },
                new List<string>() //���� ������ �̸�
                {
                    "�Ź�"
                },
                new List<Item>()
            ),
            new Item(
                "������� �Ź�",        //������ �̸�
                "�̵� �ӵ� +45, ���·� +15", //������ ����
                1100,                 //������ ����
                LoadSprite("boots"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "shoes", "move speed", "advanced shoes"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"moveSpeed", 45},
                    {"atk", 15}
                },
                new List<string>() //���� ������ �̸�
                {
                    "�Ź�"
                },
                new List<Item>()
            ),
            new Item(
                "��ö�� �Ź�",        //������ �̸�
                "�̵� �ӵ� +45, ���� +20", //������ ����
                900,                 //������ ����
                LoadSprite("05_plate_boots"), //�̹��� �̸�
                new List<string>() //������ �±�
                {
                  "shoes", "move speed", "advanced shoes"
                },
                new Dictionary<string, int>(){ //������ ����
                    {"moveSpeed", 45},
                    {"defense", 20}
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
