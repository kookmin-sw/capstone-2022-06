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
                "체력 물약",        //아이템 이름
                "10초 동안 HP +150 회복", //아이템 설명
                50,                 //아이템 가격
                LoadSprite("hp"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "potion", "comsumables", "stackable"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"hp", 150}
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
                    {"hp", 1000}
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
                    {"mp", 50}
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
                    {"mp", 200}
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
                    //{"", }
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
                    //{"", }
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
                    {"ap", 10}
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
                    {"atk", 10}
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
                    {"defense", 10}
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
                    {"moveSpeed", 10}
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
                    {"ap", 15},
                    {"hp", 70}
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
                    {"atk", 5},
                    {"hp", 100}
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
                    {"defense", 5},
                    {"hp", 100}
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
                    {"defense", 15}
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
                    {"healthRegen", 100}
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
                new Dictionary<string, int>(){ //아이템 스탯 
                    {"hp", 150}
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
                new Dictionary<string, int>(){ //아이템 스탯
                    {"atkSpeed", 12}
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
                new Dictionary<string, int>(){ //아이템 스탯 
                    {"atk", 10}
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
                new Dictionary<string, int>(){ //아이템 스탯
                    {"atk", 25}
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
                new Dictionary<string, int>(){ //아이템 스탯
                    {"coolDown", 5},
                    {"moveSpeed", 5}
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
                new Dictionary<string, int>(){ //아이템 스탯 
                    {"mp", 250}
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
                new Dictionary<string, int>(){ //아이템 스탯
                    {"manaRegen", 50}
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
                new Dictionary<string, int>(){ //아이템 스탯 
                    {"ap", 20}
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
                new Dictionary<string, int>(){ //아이템 스탯
                    {"ap", 40}
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
        {   new Item(
                "푸른빛 환영",        //아이템 이름
                "공격력 +40, 이동 속도 +5%", //아이템 설명
                1400,                 //아이템 가격
                LoadSprite("08_Blink"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "attack damage", "move speed"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"atk", 40},
                    {"moveSpeed", 5}
                },
                new List<string>() //하위 아이템 이름
                {
                    "빛의 마법봉", "찬란한 목걸이"
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
                    {"ap", 25},
                    {"coolDown", 10}
                },
                new List<string>() //하위 아이템 이름
                {
                    "마법증폭의 고서", "찬란한 목걸이"
                },
                new List<Item>()
            ),
            new Item(
                "얼음의 꽃",        //아이템 이름
                "주문력 +40, 마나 +300, 쿨타임 감소 +5%", //아이템 설명
                1300,                 //아이템 가격
                LoadSprite("06_blue_flower"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "ability power", "mana", "cooldown reduction"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"ap", 40},
                    {"mp", 300},
                    {"coolDown", 5}
                },
                new List<string>() //하위 아이템 이름
                {
                    "빛의 마법봉", "사파이어 수정"
                },
                new List<Item>()
            ),
            new Item(
                "흡수의 망토",        //아이템 이름
                "주문력 +20, 체력 +250", //아이템 설명
                1300,                 //아이템 가격
                LoadSprite("01_cloth_chest"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "ability power", "health"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"ap", 20},
                    {"hp", 250}
                },
                new List<string>() //하위 아이템 이름
                {
                    "마법증폭의 고서", "단단한 암석"
                },
                new List<Item>()
            ),
            new Item(
                "신록의 갑옷",        //아이템 이름
                "주문력 +20, 방어력 +25", //아이템 설명
                1000,                 //아이템 가격
                LoadSprite("01_plate_chest_E"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "ability power", "defense"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"ap", 20},
                    {"defense", 25}
                },
                new List<string>() //하위 아이템 이름
                {
                    "마법증폭의 고서", "천 갑옷"
                },
                new List<Item>()
            ),
            new Item(
                "역병의 꽃",        //아이템 이름
                "주문력 +25, 공격 속도 +20%", //아이템 설명
                1250,                 //아이템 가격
                LoadSprite("05_red_flower"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "ability power", "attack speed"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"ap", 25},
                    {"atkSpeed", 20}
                },
                new List<string>() //하위 아이템 이름
                {
                    "마법증폭의 고서", "신속의 검"
                },
                new List<Item>()
            ),
            new Item(
                "절정의 활",        //아이템 이름
                "주문력 +30, 공격 속도 +15%", //아이템 설명
                1300,                 //아이템 가격
                LoadSprite("01_Bow"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "ability power", "attack speed"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"ap", 30},
                    {"atkSpeed", 15}
                },
                new List<string>() //하위 아이템 이름
                {
                    "모닝스타", "신속의 검"
                },
                new List<Item>()
            ),
            new Item(
                "열정의 검",        //아이템 이름
                "공격 속도 +18%, 이동 속도 +7%", //아이템 설명
                1050,                 //아이템 가격
                LoadSprite("09_Longsword"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "move speed", "attack speed"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"atkSpeed", 18},
                    {"moveSpeed", 7}
                },
                new List<string>() //하위 아이템 이름
                {
                    "손도끼", "찬란한 목걸이"
                },
                new List<Item>()
            ),
            new Item(
                "필드의 전투",        //아이템 이름
                "공격력 +25, 쿨타임 감소 +10%", //아이템 설명
                1250,                 //아이템 가격
                LoadSprite("player_portrait_avatar_example"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "attack damage", "cooldown reduction"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"atk", 25},
                    {"coolDown", 10}
                },
                new List<string>() //하위 아이템 이름
                {
                    "모닝스타", "찬란한 목걸이"
                },
                new List<Item>()
            ),
            new Item(
                "주문포식의 지도",        //아이템 이름
                "공격력 +25, 방어력 +35", //아이템 설명
                1300,                 //아이템 가격
                LoadSprite("10_Basic_Leather"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "attack damage", "defense"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"atk", 25},
                    {"defense", 35}
                },
                new List<string>() //하위 아이템 이름
                {
                    "모닝스타", "천 갑옷"
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
                    {"atk", 15},
                    {"hp", 200}
                },
                new List<string>() //하위 아이템 이름
                {
                    "단단한 암석", "손도끼"
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
                    {"hp", 350},
                },
                new List<string>() //하위 아이템 이름
                {
                    "단단한 암석", "단단한 암석"
                },
                new List<Item>()
            ),
            new Item(
                "망령의 장갑",        //아이템 이름
                "체력 +250, 방어력 +25", //아이템 설명
                1250,                 //아이템 가격
                LoadSprite("04_plate_gloves_E"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "health", "defense"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"hp", 250},
                    {"defense", 25}
                },
                new List<string>() //하위 아이템 이름
                {
                    "단단한 암석", "천 갑옷"
                },
                new List<Item>()
            ),
            new Item(
                "열정의 방패",        //아이템 이름
                "체력 +200, 쿨타임 감소 +10%", //아이템 설명
                800,                 //아이템 가격
                LoadSprite("05_Shield"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "health", "cooldown reduction"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"hp", 200},
                    {"coolDown", 10}
                },
                new List<string>() //하위 아이템 이름
                {
                    "단단한 암석", "찬란한 목걸이"
                },
                new List<Item>()
            ),
            new Item(
                "비상의 철갑",        //아이템 이름
                "체력 +150, 이동 속도 +5%", //아이템 설명
                800,                 //아이템 가격
                LoadSprite("01_plate_chest"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "health", "move speed"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"hp", 150},
                    {"moveSpeed", 5}
                },
                new List<string>() //하위 아이템 이름
                {
                    "단단한 암석", "찬란한 목걸이"
                },
                new List<Item>()
            ),
            new Item(
                "얼음투구",        //아이템 이름
                "방어력 +20, 마나 +250, 쿨타임 감소 +10%", //아이템 설명
                900,                 //아이템 가격
                LoadSprite("Helmet_3"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "defense", "mana", "cooldown reduction"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"defense", 20},
                    {"mp", 250},
                    {"coolDown", 10}
                },
                new List<string>() //하위 아이템 이름
                {
                    "천 갑옷", "사파이어 수정"
                },
                new List<Item>()
            ),
            new Item(
                "덤불 견갑",        //아이템 이름
                "방어력 +50", //아이템 설명
                1000,                 //아이템 가격
                LoadSprite("02_plate_shoulder_E"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "defense"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"defense", 50}
                },
                new List<string>() //하위 아이템 이름
                {
                    "천 갑옷", "천 갑옷"
                },
                new List<Item>()
            ),
            new Item(
                "군단의 로브",        //아이템 이름
                "체력 +150, 방어력 +30, 기본 체력 재생 +100%", //아이템 설명
                1400,                 //아이템 가격
                LoadSprite("03_Cloth_helm_E"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "defense", "health", "health regeneration"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"hp", 150},
                    {"defense", 30},
                    {"healthRegen", 100}
                },
                new List<string>() //하위 아이템 이름
                {
                    "천 갑옷", "회복의 글로브"
                },
                new List<Item>()
            ),
            new Item(
                "금지된 반지",        //아이템 이름
                "기본 체력 재생 +100%, 기본 마나 재생 +50%", //아이템 설명
                800,                 //아이템 가격
                LoadSprite("Ring_2"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "advanced", "health regeneration", "mana regeneration"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"healthRegen", 100},
                    {"manaRegen", 50}
                },
                new List<string>() //하위 아이템 이름
                {
                    "회복의 글로브", "요정의 지도"
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
                    {"atk", 40},
                    {"hp", 300},
                    {"defense", 30}
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
                    {"atk", 45},
                    {"hp", 250},
                    {"coolDown", 10}
                },
                new List<string>() //하위 아이템 이름
                {
                    "탐식의 손도끼", "악마의 마법서"
                },
                new List<Item>()
            ),
            new Item(
                "푸른 수리검",        //아이템 이름
                "공격력 +50, 체력 +400, 기본 체력 재생 +150%", //아이템 설명
                2800,                 //아이템 가격
                LoadSprite("Shurikens"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "final", "health regeneration", "health", "attack damage"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"atk", 50},
                    {"hp", 400},
                    {"healthRegen", 150}
                },
                new List<string>() //하위 아이템 이름
                {
                    "탐식의 손도끼", "신록의 갑옷"
                },
                new List<Item>()
            ),
            new Item(
                "마나소드",        //아이템 이름
                "공격력 +35, 마나 +500, 쿨타임 감소 +15%", //아이템 설명
                2900,                 //아이템 가격
                LoadSprite("Sword_1"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "final", "cooldown reduction", "mana", "attack damage"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"atk", 35},
                    {"mana", 500},
                    {"coolDown", 15}
                },
                new List<string>() //하위 아이템 이름
                {
                    "주문포식의 지도", "얼음의 꽃"
                },
                new List<Item>()
            ),
            new Item(
                "추적자의 단검",        //아이템 이름
                "공격력 +40, 공격 속도 +40%, 방어력 +30", //아이템 설명
                3100,                 //아이템 가격
                LoadSprite("Sword_2"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "final", "attack speed", "defense", "attack damage"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"atk", 40},
                    {"atkSpeed", 40},
                    {"defense", 30}
                },
                new List<string>() //하위 아이템 이름
                {
                    "탐식의 손도끼", "절정의 활"
                },
                new List<Item>()
            ),
            new Item(
                "굶주린 폴암",        //아이템 이름
                "공격력 +70, 쿨타임 감소 +10%", //아이템 설명
                3300,                 //아이템 가격
                LoadSprite("Ax_1"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "final", "cooldown reduction", "attack damage"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"atk", 70},
                    {"coolDown", 10}
                },
                new List<string>() //하위 아이템 이름
                {
                    "필드의 전투", "절정의 활"
                },
                new List<Item>()
            ),
            new Item(
                "내셔의 곡궁",        //아이템 이름
                "주문력 +100, 공격 속도 +50%", //아이템 설명
                3000,                 //아이템 가격
                LoadSprite("Bow"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "final", "ability power", "attack speed"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"ap", 100},
                    {"atkSpeed", 50}
                },
                new List<string>() //하위 아이템 이름
                {
                    "역병의 꽃", "푸른빛 환영"
                },
                new List<Item>()
            ),
            new Item(
                "법사의 팬던트",        //아이템 이름
                "주문력 +75, 체력 +400, 기본 마나 재생 +100%", //아이템 설명
                2600,                 //아이템 가격
                LoadSprite("Amulet_1"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "final", "ability power", "health", "mana regeneration"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"ap", 75},
                    {"hp", 400},
                    {"manaRegen0", 100}
                },
                new List<string>() //하위 아이템 이름
                {
                    "얼음의 꽃", "금지된 반지"
                },
                new List<Item>()
            ),
            new Item(
                "망자의 방패",        //아이템 이름
                "체력 +300, 방어력 +45, 이동 속도 +5%", //아이템 설명
                2900,                 //아이템 가격
                LoadSprite("shield"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "final", "defense", "health", "move speed"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"hp", 300},
                    {"defense", 45},
                    {"moveSpeed", 5}
                },
                new List<string>() //하위 아이템 이름
                {
                    "거인의 투구", "비상의 철갑"
                },
                new List<Item>()
            ),
            new Item(
                "강철의 갑옷",        //아이템 이름
                "체력 +800, 체력 재생 +200%, 방어력 +30", //아이템 설명
                3500,                 //아이템 가격
                LoadSprite("Armor_2"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "final", "health regeneration", "health", "defense"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"hp", 800},
                    {"healthRegen", 200},
                    {"defense", 30}
                },
                new List<string>() //하위 아이템 이름
                {
                    "거인의 투구", "군단의 로브"
                },
                new List<Item>()
            ),
            new Item(
                "대천사의 반지",        //아이템 이름
                "주문력 +60, 체력 +200, 마나 +500", //아이템 설명
                2600,                 //아이템 가격
                LoadSprite("Ring_1"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "final", "ability power", "health", "mana"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"ap", 60},
                    {"hp", 200},
                    {"mana", 500}
                },
                new List<string>() //하위 아이템 이름
                {
                    "빛의 마법봉", "흡수의 망토"
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
                    {"moveSpeed", 25}
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
                    {"moveSpeed", 60}
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
                    {"moveSpeed", 45},
                    {"coolDown", 10}
                },
                new List<string>() //하위 아이템 이름
                {
                    "신발"
                },
                new List<Item>()
            ),
            new Item(
                "광전사의 신발",        //아이템 이름
                "이동 속도 +45, 공격 속도 +30%", //아이템 설명
                1100,                 //아이템 가격
                LoadSprite("05_plate_boots_E"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "shoes", "move speed", "advanced shoes"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"moveSpeed", 45},
                    {"atkSpeed", 30}
                },
                new List<string>() //하위 아이템 이름
                {
                    "신발"
                },
                new List<Item>()
            ),
            new Item(
                "마법사의 신발",        //아이템 이름
                "이동 속도 +45, 주문력 +15", //아이템 설명
                1100,                 //아이템 가격
                LoadSprite("05_cloth_boots"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "shoes", "move speed", "advanced shoes"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"moveSpeed", 45},
                    {"ap", 15}
                },
                new List<string>() //하위 아이템 이름
                {
                    "신발"
                },
                new List<Item>()
            ),
            new Item(
                "용맹함의 신발",        //아이템 이름
                "이동 속도 +45, 공력력 +15", //아이템 설명
                1100,                 //아이템 가격
                LoadSprite("boots"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "shoes", "move speed", "advanced shoes"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"moveSpeed", 45},
                    {"atk", 15}
                },
                new List<string>() //하위 아이템 이름
                {
                    "신발"
                },
                new List<Item>()
            ),
            new Item(
                "강철의 신발",        //아이템 이름
                "이동 속도 +45, 방어력 +20", //아이템 설명
                900,                 //아이템 가격
                LoadSprite("05_plate_boots"), //이미지 이름
                new List<string>() //아이템 태그
                {
                  "shoes", "move speed", "advanced shoes"
                },
                new Dictionary<string, int>(){ //아이템 스탯
                    {"moveSpeed", 45},
                    {"defense", 20}
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
