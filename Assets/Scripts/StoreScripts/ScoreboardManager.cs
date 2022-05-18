using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardManager : MonoBehaviour
{
    ShopManager SM;

    public GameObject ScoreBoard;

    public Text Red_GoldText;
    public Text Red_TowerText;
    public Text Red_KillText;

    public Text Blue_GoldText;
    public Text Blue_TowerText;
    public Text Blue_KillText;

    public int redGlobalGold;
    public int redTowerLeft;
    public int redTotalKill;

    public int blueGlobalGold;
    public int blueTowerLeft;
    public int blueTotalKill;

    public GameObject ChampionInfoPrefab;
    public List<GameObject> RedChampionInfos;
    public List<GameObject> BlueChampionInfos;

    public Transform RedChampionInfoTransform;
    public Transform BlueChampionInfoTransform;
    // Start is called before the first frame update
    void Start()
    {
        ScoreBoard.SetActive(true);
        SM = GameObject.Find("ShopManager").GetComponent<ShopManager>();
        ChampionAdd();
        SM.UpdateInventories();
        ScoreBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Red_GoldText.text = redGlobalGold.ToString();
        Red_TowerText.text = redTowerLeft.ToString();
        Red_KillText.text = redTotalKill.ToString();

        Blue_GoldText.text = blueGlobalGold.ToString();
        Blue_TowerText.text = blueTowerLeft.ToString();
        Blue_KillText.text = blueTotalKill.ToString();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ScoreBoard.SetActive(true);
            SM.UpdateInventories();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ScoreBoard.SetActive(false);
        }
    }
    
    //�� �κ��� ���Ŀ� �����Ͻô� ���� ���� �� �����ϴ�
    public void ChampionInfoSetting()
    {

    }
    public void ChampionAdd()
    {
        GameObject player = Instantiate(ChampionInfoPrefab, RedChampionInfoTransform);
        RedChampionInfos.Add(player);
        SM.CharacterSlotTransforms.Add(player.GetComponent<CharacterSlot>().InventorySlotTransform);
    }
}
