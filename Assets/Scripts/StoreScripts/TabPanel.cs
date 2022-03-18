using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabPanel : MonoBehaviour
{

    // item ī�װ� select �� ��� ���� -> �ƴҽ� �⺻ ������� ����
    public List<TabButton> tabButtons;
    public List<GameObject> contentsPanels;
    
    public void ClickTab(int id)
    {
        for(int i=0; i<contentsPanels.Count; i++)
        {
            if (i == id)
            {
                contentsPanels[i].SetActive(true);
                tabButtons[i].Selected();
            }
            else
            {
                contentsPanels[i].SetActive(false);
                tabButtons[i].DeSelected();
            }
        }
    }
}
