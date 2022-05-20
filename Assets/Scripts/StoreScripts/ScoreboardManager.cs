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
        StartCoroutine("ChampionAdd");
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
    
    //이 부분은 이후에 구현하시는 편이 나을 것 같습니다
    public void ChampionInfoSetting()
    {

    }
    public IEnumerator ChampionAdd()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in players)
        {
            if (p.layer == LayerMask.GetMask("RedTeam"))
            {
                GameObject player = Instantiate(ChampionInfoPrefab, RedChampionInfoTransform);
                player.GetComponent<CharacterSlot>().player = p;
                RedChampionInfos.Add(player);
                SM.CharacterSlotTransforms.Add(player.GetComponent<CharacterSlot>().InventorySlotTransform);
            }
            else
            {
                GameObject player = Instantiate(ChampionInfoPrefab, BlueChampionInfoTransform);
                player.GetComponent<CharacterSlot>().player = p;
                BlueChampionInfos.Add(player);
                SM.CharacterSlotTransforms.Add(player.GetComponent<CharacterSlot>().InventorySlotTransform);
            }
        }
    }
}
