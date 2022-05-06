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
                "체력 물약",        //아이템 이름
                "10초 동안 HP +150 회복", //아이템 설명
                50,                 //아이템 가격
                LoadSprite("hp"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "potion", "comsumables", "stackable"
                },
                new Dictionary<string, int>(){ //아이템 스탯 (이후 게임 구현하실 때 활용하시면 됩니다!)
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "더 큰 체력 물약", //아이템 이름
                "10초 동안 HP +1000 회복", //아이템 설명
                250, //아이템 가격
                LoadSprite("Elixir_1"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "potion", "comsumables", "stackable"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "마나 물약", //아이템 이름
                "10초 동안 MP +50 회복", //아이템 설명
                100, //아이템 가격
                LoadSprite("mp"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "potion", "comsumables", "stackable"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "더 큰 마나 물약", //아이템 이름
                "10초 동안 MP +200 회복", //아이템 설명
                300, //아이템 가격
                LoadSprite("Elixir_2"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "potion", "comsumables", "stackable"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "와드 토템", //아이템 이름
                "아군 모두에게 90~120초 동안 주변 지역을 밝혀 주는 투명 와드 하나를 지면에 설치합니다.", //아이템 설명
                0, //아이템 가격
                LoadSprite("Arcanist15"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "ward",  "comsumables"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "제어의 룬", //아이템 이름
                "근처의 와드토템을 볼 수 있습니다.", //아이템 설명
                0, //아이템 가격
                LoadSprite("Stone_1"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "ward", "comsumables"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "마법의 영약", //아이템 이름
                "3분 동안 주문력 +10", //아이템 설명
                500, //아이템 가격
                LoadSprite("Elixir_4"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "elixir", "comsumables"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "분노의 영약", //아이템 이름
                "3분 동안 공격력 +10", //아이템 설명
                500, //아이템 가격
                LoadSprite("Elixir_6"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "elixir", "comsumables"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "강철의 영약", //아이템 이름
                "3분 동안 방어력 +10", //아이템 설명
                500, //아이템 가격
                LoadSprite("Elixir_5"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "elixir", "comsumables"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "신속의 영약", //아이템 이름
                "3분 동안 이동속도 + 10", //아이템 설명
                500, //아이템 가격
                LoadSprite("Elixir_3"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "elixir", "comsumables"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
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
                "시작의 반지",        //아이템 이름
                "주문력 +15, 체력 +70", //아이템 설명
                450,                 //아이템 가격
                LoadSprite("rings"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "basic"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "시작의 검",        //아이템 이름
                "공격력 +5, 체력 +100", //아이템 설명
                450,                 //아이템 가격
                LoadSprite("sword"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "basic"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "시작의 방패",        //아이템 이름
                "방어력 +5, 체력 +100", //아이템 설명
                450,                 //아이템 가격
                LoadSprite("shield_2"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "basic"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "천 갑옷",        //아이템 이름
                "방어력 +15", //아이템 설명
                300,                 //아이템 가격
                LoadSprite("armor"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "basic", "defense"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "회복의 글로브",        //아이템 이름
                "기본 체력 재생 +100%", //아이템 설명
                300,                 //아이템 가격
                LoadSprite("04_cloth_gloves_E"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "basic", "health regeneration"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "단단한 암석",        //아이템 이름
                "체력 +150", //아이템 설명
                400,                 //아이템 가격
                LoadSprite("02_Iron_Ore"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "basic", "health"
                },
                new Dictionary<string, int>(){ //아이템 스탯 (이후 게임 구현하실 때 활용하시면 됩니다!)
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "신속의 검",        //아이템 이름
                "공격 속도 +12% ", //아이템 설명
                300,                 //아이템 가격
                LoadSprite("02_Sword"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "basic", "attack speed"
                },
                new Dictionary<string, int>(){ //아이템 스탯 (이후 게임 구현하실 때 활용하시면 됩니다!)
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "손도끼",        //아이템 이름
                "공격력 +10", //아이템 설명
                350,                 //아이템 가격
                LoadSprite("06_One_Handed_Axe"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "basic", "attack damage"
                },
                new Dictionary<string, int>(){ //아이템 스탯 (이후 게임 구현하실 때 활용하시면 됩니다!)
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "모닝스타",        //아이템 이름
                "공격력 +25", //아이템 설명
                875,                 //아이템 가격
                LoadSprite("04_Mace_with_spikes"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "basic", "attack damage"
                },
                new Dictionary<string, int>(){ //아이템 스탯 (이후 게임 구현하실 때 활용하시면 됩니다!)
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "찬란한 목걸이",        //아이템 이름
                "쿨타임 감소 +5%, 이동 속도 +5%", //아이템 설명
                300,                 //아이템 가격
                LoadSprite("necklace"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "basic", "cooldown reduction", "move speed"
                },
                new Dictionary<string, int>(){ //아이템 스탯 (이후 게임 구현하실 때 활용하시면 됩니다!)
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "사파이어 수정",        //아이템 이름
                "마나 +250", //아이템 설명
                350,                 //아이템 가격
                LoadSprite("gem"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "basic", "mana"
                },
                new Dictionary<string, int>(){ //아이템 스탯 (이후 게임 구현하실 때 활용하시면 됩니다!)
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "요정의 지도",        //아이템 이름
                "기본 마나 재생 +50%", //아이템 설명
                250,                 //아이템 가격
                LoadSprite("scroll"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "basic", "mana regeneration"
                },
                new Dictionary<string, int>(){ //아이템 스탯 (이후 게임 구현하실 때 활용하시면 됩니다!)
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "마법증폭의 고서",        //아이템 이름
                "주문력 +20", //아이템 설명
                435,                 //아이템 가격
                LoadSprite("book"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "basic", "ability power"
                },
                new Dictionary<string, int>(){ //아이템 스탯 (이후 게임 구현하실 때 활용하시면 됩니다!)
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {

                },
                new List<Item>()
            ),
            new Item(
                "빛의 마법봉",        //아이템 이름
                "주문력 +40", //아이템 설명
                850,                 //아이템 가격
                LoadSprite("08_Mage_Staff"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "basic", "ability power"
                },
                new Dictionary<string, int>(){ //아이템 스탯 (이후 게임 구현하실 때 활용하시면 됩니다!)
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
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
                "주문포식의 지도",        //아이템 이름
                "공격력 +25,방어력 +35", //아이템 설명
                1300,                 //아이템 가격
                LoadSprite("10_Basic_Leather"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "attack damage", "defense"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {
                    "모닝스타", "천 갑옷"
                },
                new List<Item>()
            ),
            new Item(
                "거인의 투구",        //아이템 이름
                "체력 +350", //아이템 설명
                900,                 //아이템 가격
                LoadSprite("03_Leather_helm_E"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "health"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {
                    "단단한 암석", "단단한 암석"
                },
                new List<Item>()
            ),
            new Item(
                "탐식의 손도끼",        //아이템 이름
                "공격력 +15, 체력 +200", //아이템 설명
                1100,                 //아이템 가격
                LoadSprite("03_Greataxe"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "health", "attack damage"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {
                    "단단한 암석", "손도끼"
                },
                new List<Item>()
            ),
            new Item(
                "악마의 마법서",        //아이템 이름
                "주문력 +25, 쿨타임 감소 +10%", //아이템 설명
                900,                 //아이템 가격
                LoadSprite("Enchanter18"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "ability power", "cooldown reduction"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {
                    "마법증폭의 고서", "찬란한 목걸이"
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
                "금빛 여명",        //아이템 이름
                "공격력 +40, 체력 +300, 방어력 +30 ", //아이템 설명
                3000,                 //아이템 가격
                LoadSprite("Ax_2"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "final", "defense", "health", "attack damage"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {
                    "주문포식의 지도", "거인의 투구"
                },
                new List<Item>()
            ),
            new Item(
                "절명의 투구",        //아이템 이름
                "공격력 +45, 체력 +250, 쿨타임 감소 +10%", //아이템 설명
                2600,                 //아이템 가격
                LoadSprite("Helmet_4"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "final", "cooldown reduction", "health", "attack damage"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {
                    "탐식의 손도끼", "악마의 마법서"
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
                "신발",        //아이템 이름
                "이동 속도 +25", //아이템 설명
                300,                 //아이템 가격
                LoadSprite("05_leather_boots"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "shoes", "move speed", "base shoes"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {
                    
                },
                new List<Item>()
            ),
            new Item(
                "신속의 신발",        //아이템 이름
                "이동 속도 + 60", //아이템 설명
                900,                 //아이템 가격
                LoadSprite("05_cloth_boots_E"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "shoes", "move speed", "advanced shoes"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {
                    "신발"
                },
                new List<Item>()
            ),
            new Item(
                "명석함의 신발",        //아이템 이름
                "이동 속도 + 45, 스킬 쿨타임 감소 +10%", //아이템 설명
                950,                 //아이템 가격
                LoadSprite("05_leather_boots_E"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "shoes", "move speed", "advanced shoes"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    //{"str", 1},
                },
                new List<string>() //하위 아이템 이름
                {
                    "신발"
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
