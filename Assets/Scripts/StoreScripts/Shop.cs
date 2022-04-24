using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    // Store에 등록할 item의 정보를 담을 배열들
    public Sprite[] itemSprite;
    public GameObject[] itemObj;
    public int[] itemPrice;
    public string[] itemDescriptionArr;

    [SerializeField]
    private Description itemDescription;

    [SerializeField]
    private ItemTree itemTree;
    
    [SerializeField]
    private Inventory inventory;

    private void Awake() // 초기화
    {
        itemDescription.ResetDescription();
        itemTree.ResetDescription();
    }
    //선택한 아이템 정보들 저장 변수
    public Sprite image;
    public string itemprice, title, description;

    //구매시 해당 item 정보 저장 후 다른 파일의 함수 호출
    public void Buy(int index)
    {
        int price = itemPrice[index];
        image = itemSprite[index];
        title = itemObj[index].name;
        itemprice = itemPrice[index].ToString();
        description = itemDescriptionArr[index];
        itemDescription.SetDescription(image, itemprice, title, description);
        itemTree.SetDescription(image, itemprice);
        // 가진 돈보다 비싸다면 못사게 함
        if (price > Gold.playerGold || inventory.isPull == true)
        {
            return;
        }

        else
        {
            inventory.AddItem(image, itemprice);
            Gold.playerGold -= price;
        }
    }
}
