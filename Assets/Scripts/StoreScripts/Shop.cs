using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    // Store�� ����� item�� ������ ���� �迭��
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

    private void Awake() // �ʱ�ȭ
    {
        itemDescription.ResetDescription();
        itemTree.ResetDescription();
    }
    //������ ������ ������ ���� ����
    public Sprite image;
    public string itemprice, title, description;

    //���Ž� �ش� item ���� ���� �� �ٸ� ������ �Լ� ȣ��
    public void Buy(int index)
    {
        int price = itemPrice[index];
        image = itemSprite[index];
        title = itemObj[index].name;
        itemprice = itemPrice[index].ToString();
        description = itemDescriptionArr[index];
        itemDescription.SetDescription(image, itemprice, title, description);
        itemTree.SetDescription(image, itemprice);
        // ���� ������ ��δٸ� ����� ��
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
