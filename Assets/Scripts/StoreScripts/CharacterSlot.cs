using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public GameObject player;

    public Image CharacterImage;
    public Image SpellImage1;
    public Image SpellImage2;

    public Text KText;
    public Text DText;
    public Text CSText;

    public Transform InventorySlotTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KText.text = player.GetComponent<ChampionManager>().killCount.ToString();
        DText.text = player.GetComponent<ChampionManager>().deathCount.ToString();
    }
}
