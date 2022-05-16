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
    private PhotonView PV;

    private string iconJsonPath = "Data/HeroIcons";
    private string initPortraitPath = "Private/Textures/Layout/empty_hero_spot";

    // 지휘관이 될 룸 id
    // private int[] commanderSlot = {1, 6};
    private int[] commanderSlot = {5, 10};

    private GameObject contentsDiv = null;

    private int myActorId = -1;
    private Sprite initPortrait = null;

    // 픽창에서 자신이 위치한 슬롯을 나타냄
    private GameObject myState = null;

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

        // 룸 프로퍼티 (ready count 보관)
        if (PV.IsMine)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() {
                {"readyCount", 0}
            });
        }

        // 플레이어 프로터피 초기화(선택한 챔피언 정보를 담기 위한)
        UpdateProperty(new Hashtable());

        myActorId = GetActorId();
    }

    void Update()
    {
        // int cnt = 0;
        // int num = 1;
        // foreach (Player player in PhotonNetwork.PlayerList)
        // {
        //     object tmp;
        //     if (player.CustomProperties.TryGetValue(_ready, out tmp))
        //     {
        //         Debug.Log($"num : {num}, ready : {(int)tmp}");
        //         cnt += (int)tmp;
        //     }
        //     else
        //     {
        //         Debug.Log($"num : {num}, Not valid value");
        //     }
        //     num++;
        // }
        // Debug.Log(cnt);
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        GetButton((int)Buttons.UI_ConfirmButton).interactable = false;

        initPortrait = Managers.Resource.Load<Sprite>(initPortraitPath);

        contentsDiv = Util.SearchChild(
            Util.SearchChild(Get<GameObject>((int)GameObjects.PickUpBoard), "Viewport"),
            "ContentsDiv"
        );

        if (PV.IsMine)
        {
            PV.RPC("ClearDummyPortraits", RpcTarget.AllBuffered);

            TextAsset jsonText = Managers.Resource.Load<TextAsset>(iconJsonPath);

            if (jsonText)
            {
                PV.RPC("initializePortraits", RpcTarget.AllBuffered, jsonText.ToString());
            }
            else
            {
                Debug.LogError($"Failed to load json file {iconJsonPath}");
            }  
        }

        myState = Util.SearchChild(
            Get<GameObject>((int)GameObjects.SelectedView),
            $"Player_{myActorId}"
        );

        // ConfirmButton을 눌렀을 때 ConfirmButton을 비활성화 하고 CancelButton을 활성화 합니다.
        GetButton((int)Buttons.UI_ConfirmButton).onClick.AddListener(() => {
            GetButton((int)Buttons.UI_ConfirmButton).gameObject.SetActive(false);
            GetButton((int)Buttons.UI_CancelButton).gameObject.SetActive(true);

            StartCoroutine(UpdateReadyCount(1));

            if (PV.IsMine)
            {
                StartCoroutine("WaitAllReady");
            }
        });

        // CancelButton을 눌렀을 때 CancelButton을 비활성화 하고 ConfirmButton을 활성화 합니다.
        GetButton((int)Buttons.UI_CancelButton).onClick.AddListener(() => {
            GetButton((int)Buttons.UI_CancelButton).gameObject.SetActive(false);
            GetButton((int)Buttons.UI_ConfirmButton).gameObject.SetActive(true);
            GetButton((int)Buttons.UI_ConfirmButton).interactable = false;

            StartCoroutine(UpdateReadyCount(-1));
            UpdateProperty(new Hashtable());

            PV.RPC("UpdatePortrait", RpcTarget.AllBuffered, myActorId, initPortraitPath);

            if (PV.IsMine)
            {
                StopCoroutine("WaitAllReady");
            }
        });

        // 지휘관이면 챔피언 선택을 막고 전용 문구를 보여줍니다.
        // 또한 초상화를 지휘관 초상화로 변경합니다.
        // 챔피언을 선택할 수 없게 모든 버튼을 inactive로 바꿉니다.
        if (myActorId == commanderSlot[0] || myActorId == commanderSlot[1])
        {
            GetButton((int)Buttons.UI_CancelButton).gameObject.SetActive(false);
            GetButton((int)Buttons.UI_ConfirmButton).gameObject.SetActive(false);
            GetText((int)Texts.ComStatement).gameObject.SetActive(true);
            PV.RPC("UpdatePortrait", RpcTarget.All, myActorId, "Private/Textures/Icons/HeadKing");

            foreach (Transform child in contentsDiv.transform)
            {
                Button btn = child.gameObject.GetComponent<Button>();
                if (btn)
                {
                    btn.interactable = false;
                }
            }

            StartCoroutine(UpdateReadyCount(1));

            Hashtable myHash = new Hashtable() {
                {"isExists", true},
                {"isCommander", true},
                {"actorId", myActorId}
            };
            UpdateProperty(myHash);

            if (PV.IsMine)
            {
                StartCoroutine("WaitAllReady");
            }
        }
    }

    /// <summary>
    /// 자신이 만들어진 room에서 몇번째로 들어왔는지 찾습니다.
    /// 자신의 아이디를 찾지 못하면 -1을 반환합니다.
    /// </summray>
    private int GetActorId()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            Player p = PhotonNetwork.PlayerList[i];
            if (p.UserId == PhotonNetwork.LocalPlayer.UserId)
            {
                // Debug.LogError($"My local id is {i + 1}");
                return i + 1;
            }
        }
        return -1;
    }

    /// <summary>
    /// 모든 플레이어가 준비가 될 때까지 기다리는 코루틴입니다.
    /// 중간 탈주로 인원이 비어 시작하지 못하는 경우를 방지하기 위해 PlayerCount로 검사합니다.
    /// </summary>
    private IEnumerator WaitAllReady()
    {
        yield return new WaitUntil(() => {
            object ret;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("readyCount", out ret))
            {
                return (int)ret == PhotonNetwork.CurrentRoom.PlayerCount;
            }
            else
            {
                return false;
            }
        });

        Debug.Log($"All player get ready : {PhotonNetwork.CurrentRoom.MaxPlayers}");
        PhotonNetwork.Destroy(gameObject);
        PhotonNetwork.LoadLevel(2);
    }

    /// <summary>
    /// 룸 프로퍼티의 readyCount를 1만큼 변경하는 메서드
    /// 지연시간을 고려하여 코루틴으로 작성
    /// </summary>
    private IEnumerator UpdateReadyCount(int diff)
    {
        yield return new WaitUntil(() => {
            object tmp;
            return PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("readyCount", out tmp);
        });

        object tmp;
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("readyCount", out tmp))
        {
            Debug.Log($"Room player count changed by {diff}");
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() {
                {"readyCount", (int)tmp + diff}
            });
        }
    }

    /// <summary>
    /// 로컬 플레이어의 프로퍼티를 갱신하기 위한 편의 메서드
    /// </summary>
    private void UpdateProperty(Hashtable customProp)
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProp);
    }

    /// <summary>
    /// 자신의 초상화를 선택한 영웅의 초상화로 바꿉니다.
    /// RPC로 다른 플레이어에게도 초상화가 바뀌었음을 명시합니다.
    /// </summary>
    [PunRPC]
    void UpdatePortrait(int playerId, string spritePath)
    {
        GameObject slot = Util.SearchChild(
            Get<GameObject>((int)GameObjects.SelectedView),
            $"Player_{playerId}",
            true
        );
        GameObject portrait = Util.SearchChild(slot, "Portrait");
        portrait.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>(spritePath);
    }

    /// <summary>
    /// 선택창에 있던 초상화를 제거합니다.
    /// </summary>
    [PunRPC]
    void ClearDummyPortraits()
    {
        foreach (Transform child in contentsDiv.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// 마스터 클라이언트가 파싱한 json으로 챔피언 데이터를 로드하여 다른 클라이언트와 동기화 합니다.
    /// </summary>
    [PunRPC]
    void initializePortraits(string jsonParsed)
    {
        Icons loadedIcons = JsonUtility.FromJson<Icons>(jsonParsed);
        for (int i = 0; i < loadedIcons.Contents.Count; i++)
        {
            HeroInfo info = loadedIcons.Contents[i];
            Sprite heroPortrait = Managers.Resource.Load<Sprite>(info.spritePath);
            UI_PortraitButton portrait = Managers.UI.AttachSubItem<UI_PortraitButton>(contentsDiv.transform);

            // #Critical
            // 버튼에 달린 PortraitButtonData 컴포넌트에 챔피언 이름과 프리팹 경로, 그리고 스프라이트 경로를 할당합니다.
            portrait.GetComponent<Image>().sprite = heroPortrait;
            portrait.name = $"Portrait{i + 1}";
            PortraitButtonData _data = portrait.GetComponent<PortraitButtonData>();
            _data.heroName = info.heroName;
            _data.prefabPath = info.prefabPath;
            _data.spritePath = info.spritePath;

            // 버튼을 눌렀을 때 selectedPortrait를 갱신하는 OnClick 콜백을 추가합니다.
            // 자기 픽 슬롯의 portrait도 변경할 수 있도록 합니다.
            // 추가로 ConfirmButton을 누를 수 있도록 합니다.
            // 플레이어 프로퍼티를 갱신합니다.
            portrait.GetComponent<Button>().onClick.AddListener(() => {
                GameObject selected = EventSystem.current.currentSelectedGameObject;
                GetButton((int)Buttons.UI_ConfirmButton).interactable = true;
                PortraitButtonData selectedData = selected.GetComponent<PortraitButtonData>();
                string _spritePath = selectedData.spritePath;
                string _prefabPath = selectedData.prefabPath;
                string _champName = selectedData.heroName;

                Hashtable myHash = new Hashtable() {
                    {"isExists", true},
                    {"isCommander", false},
                    {"prefabPath", _prefabPath},
                    {"championName", _champName},
                    {"actorId", myActorId}
                };
                UpdateProperty(myHash);

                PV.RPC("UpdatePortrait", RpcTarget.All, myActorId, _spritePath);
            });
        }
    }
}
