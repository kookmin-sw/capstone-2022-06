using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

[System.Serializable]
class Icons
{
    public List<HeroInfo> Contents;
}

[System.Serializable]
class HeroInfo 
{
    public string spritePath;
    public string heroName;
    public string prefabPath;
}

public class UI_Preparation : UI_Scene
{
    public Hashtable _playerCustomProperties = new Hashtable();
    private PhotonView PV;

    private string iconJsonPath = "Assets/Scripts/JSON/HeroIcons.json";
    private string initPortraitPath = "Private/Textures/Layout/empty_hero_spot";

    // 지휘관이 될 룸 id
    // private int[] commanderSlot = {1, 6};
    private int[] commanderSlot = {5, 10};

    private GameObject contentsDiv = null;

    private int myLocalId = -1;
    private Sprite initPortrait = null;
    private Sprite selectedPortrait = null;

    // 픽창에서 자신이 위치한 칸을 나타냄
    private GameObject myState = null;

    // 준비가 완료된 사람 수
    private int preparedCount = 0;

    private IEnumerator readyStatus;

    enum GameObjects
    {
        SelectedView,
        PickUpBoard
    }

    enum Buttons
    {
        UI_ConfirmButton,
        UI_CancelButton
    }

    enum Texts
    {
        ComStatement
    }

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        PhotonNetwork.LocalPlayer.SetCustomProperties(_playerCustomProperties);
        _playerCustomProperties["PlayerReady"] = 0;
    }
    
    void Update()
    {
        if (preparedCount != PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            preparedCount = 0;

            foreach (Player player in PhotonNetwork.PlayerList)
            {
                object tmp;
                if (player.CustomProperties.TryGetValue("PlayerReady", out tmp))
                {
                    // Debug.Log($"num : {myLocalId}, ready : {(int)tmp}");
                    ++preparedCount;
                }
                else
                {
                    // Debug.Log($"num : {playerNum}, Not valid value");
                }
            }
        }
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        initPortrait = Managers.Resource.Load<Sprite>(initPortraitPath);

        contentsDiv = Util.SearchChild(
            Util.SearchChild(Get<GameObject>((int)GameObjects.PickUpBoard), "Viewport"),
            "ContentsDiv"
        );

        myLocalId = GetLocalId();

        myState = Util.SearchChild(
            Get<GameObject>((int)GameObjects.SelectedView),
            $"Player_{myLocalId}"
        );

        // 지휘관이면 챔피언 선택을 막고 전용 문구를 보여줍니다.
        // 또한 초상화를 지휘관 초상화로 변경합니다.
        if (myLocalId == commanderSlot[0] || myLocalId == commanderSlot[1])
        {
            GetButton((int)Buttons.UI_CancelButton).gameObject.SetActive(false);
            GetButton((int)Buttons.UI_ConfirmButton).gameObject.SetActive(false);
            GetText((int)Texts.ComStatement).gameObject.SetActive(true);
            PV.RPC("SetCommanderPortrait", RpcTarget.All);

            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LocalPlayer.CustomProperties["PlayerReady"] = 1;
            }
            else
            {
                PV.RPC("UpdateReadyState", RpcTarget.All, 1);
            }
            
            StartCoroutine("WaitAllReady");
        }

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
                Sprite heroPortrait = Managers.Resource.Load<Sprite>(info.spritePath);
                UI_PortraitButton portrait = Managers.UI.AttachSubItem<UI_PortraitButton>(contentsDiv.transform);

                portrait.GetComponent<Image>().sprite = heroPortrait;
                portrait.name = $"Portrait{i + 1}";
                portrait.GetComponent<PortraitButtonData>().HeroName = info.heroName;
                portrait.GetComponent<PortraitButtonData>().PrefabPath = info.prefabPath;

                // 버튼을 눌렀을 때 selectedPortrait를 갱신하는 OnClick 콜백을 추가합니다.
                portrait.GetComponent<Button>().onClick.AddListener(() => {
                    GameObject selected = EventSystem.current.currentSelectedGameObject;
                    Sprite selectedSprite = selected.GetComponent<Image>().sprite;
                    selectedPortrait = selectedSprite;
                });
            }
        }
        else
        {
            Debug.Log($"Failed to load json file {jsonInfo.Name}");
        }

        // ConfirmButton을 눌렀을 때 ConfirmButton을 비활성화 하고 CancelButton을 활성화 합니다.
        GetButton((int)Buttons.UI_ConfirmButton).onClick.AddListener(() => {
            GetButton((int)Buttons.UI_ConfirmButton).gameObject.SetActive(false);
            GetButton((int)Buttons.UI_CancelButton).gameObject.SetActive(true);

            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LocalPlayer.CustomProperties["PlayerReady"] = 1;
                GameObject portrait = Util.SearchChild(myState, "Portrait");
                portrait.GetComponent<Image>().sprite = selectedPortrait;
            }
            else
            {
                PV.RPC("UpdateReadyState", RpcTarget.All, 1);
            }

            StartCoroutine("WaitAllReady");
        });

        // CancelButton을 눌렀을 때 CancelButton을 비활성화 하고 ConfirmButton을 활성화 합니다.
        GetButton((int)Buttons.UI_CancelButton).onClick.AddListener(() => {
            GetButton((int)Buttons.UI_CancelButton).gameObject.SetActive(false);
            GetButton((int)Buttons.UI_ConfirmButton).gameObject.SetActive(true);

            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LocalPlayer.CustomProperties["PlayerReady"] = 0;
                GameObject portrait = Util.SearchChild(myState, "Portrait");
                portrait.GetComponent<Image>().sprite = initPortrait;
            }
            else
            {
                PV.RPC("UpdateReadyState", RpcTarget.All, 0);
            }

            StopCoroutine("WaitAllReady");
        });
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

    /// <summary>
    /// RPC로 PlayerReady 프로퍼티를 갱신합니다.
    /// 마스터 클라이언트가 아닌 경우에만 적용됩니다.
    /// </summary>
    [PunRPC]
    void UpdateReadyState(int ready)
    {
        PhotonNetwork.LocalPlayer.CustomProperties["PlayerReady"] = ready;

        if (ready == 0)
        {
            GameObject portrait = Util.SearchChild(myState, "Portrait");
            portrait.GetComponent<Image>().sprite = initPortrait;
        }
    }

    /// <summary>
    /// 자신의 초상화를 선택한 영웅의 초상화로 바꿉니다.
    /// RPC로 다른 플레이어에게도 초상화가 바뀌었음을 명시합니다.
    /// </summary>
    [PunRPC]
    void UpdatePortrait(int playerId)
    {
        GameObject portrait = Util.SearchChild(myState, "Portrait");
        portrait.GetComponent<Image>().sprite = selectedPortrait;
    }

    /// <summary>
    /// 자신의 초상화를 선택한 지휘관 초상화로 바꿉니다.
    /// RPC로 다른 플레이어에게도 초상화가 바뀌었음을 명시합니다.
    /// </summary>
    [PunRPC]
    void SetCommanderPortrait()
    {
        GameObject portrait = Util.SearchChild(myState, "Portrait");
        portrait.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Private/Textures/Icons/HeadKing");
    }
}
