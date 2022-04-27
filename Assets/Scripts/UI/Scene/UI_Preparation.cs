using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using HashTable = ExitGames.Client.Photon.Hashtable;

[System.Serializable]
class Icons
{
    public List<HeroInfo> Contents;
}

[System.Serializable]
class HeroInfo 
{
    public string path;
}

public class UI_Preparation : UI_Scene
{
    private PhotonView PV;

    private GameObject contentsDiv = null;

    private string iconJsonPath = "Assets/Scripts/JSON/HeroIcons.json";
    private int myLocalId = -1;
    private int preparedCount = 0;
    private string selectedIcon = null;
    
    public Hashtable _playerCustomProperties = new Hashtable();

    enum GameObjects
    {
        SelectedView,
        PickUpBoard,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        contentsDiv = Util.SearchChild(
            Util.SearchChild(Get<GameObject>((int)GameObjects.PickUpBoard), "Viewport"),
            "ContentsDiv"
        );

        PV = GetComponent<PhotonView>();
        myLocalId = GetLocalId();

        // Delete dummy icons
        foreach (Transform child in contentsDiv.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        FileInfo jsonInfo = new FileInfo(iconJsonPath);

        // 찾는 json 파일이 존재하는 경우 파싱해서 초상화를 불러옵니다.
        if (jsonInfo.Exists)
        {
            string jsonString = File.ReadAllText(jsonInfo.FullName);
            Icons loadedIcons = JsonUtility.FromJson<Icons>(jsonString);

            for (int i = 0; i < loadedIcons.Contents.Count; i++)
            {
                HeroInfo info = loadedIcons.Contents[i];
                Sprite heroPortrait = Managers.Resource.Load<Sprite>(info.path);
                UI_PortraitButton portrait = Managers.UI.AttachSubItem<UI_PortraitButton>(contentsDiv.transform);

                portrait.GetComponent<Image>().sprite = heroPortrait;
                portrait.name = $"Portrait{i + 1}";

                // 버튼을 눌렀을 때 selectedIcon의 내용을 갱신하는 OnClick 콜백을 추가합니다.
                portrait.GetComponent<Button>().onClick.AddListener(() => {
                    GameObject selected = EventSystem.current.currentSelectedGameObject;
                    selectedIcon = selected.GetComponent<Sprite>().name;
                });
            }
        }
        else
        {
            Debug.Log($"Failed to load json file {jsonInfo.Name}");
        }

        // Managers.UI.AttachSubItem
    }

    /// <summary>
    /// 자신이 만들어진 room에서 몇번째로 들어왔는지 찾습니다.
    /// 자신의 아이디를 찾지 못하면 -1을 반환합니다.
    /// </summray>
    private int GetLocalId()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            Player p = PhotonNetwork.PlayerList[i];
            if (p.UserId == PhotonNetwork.LocalPlayer.UserId)
            {
                return i + 1;
            }
        }

        return -1;
    }

    /// <summary>
    /// 모든 플레이어가 영웅을 선택할지 기다리는 코루틴입니다.
    /// </summary>
    private IEnumerator WaitAllReady()
    {
        yield return new WaitUntil(() => {
            return preparedCount == PhotonNetwork.CurrentRoom.PlayerCount;
        });

        Debug.Log($"All player get ready : {preparedCount}");
        PhotonNetwork.LoadLevel("GameScene");
    }
}
