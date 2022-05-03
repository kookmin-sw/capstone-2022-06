using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class UI_Lobby : UI_Scene
{
    private GameObject matchmakingPanel;
    private Button matchmakingButton;
    private Button cancelButton;
    private Button testGameButton;
    private byte _maxPlayers = 2;
    
    enum GameObjects
    {
        MatchmakingPanel,
    }

    enum Texts
    {
        SearchingText,
        ConnectingText
    }

    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));
    
        matchmakingPanel = Get<GameObject>((int)GameObjects.MatchmakingPanel);
        Get<Text>((int)Texts.SearchingText).gameObject.SetActive(false);
        GetText((int)Texts.ConnectingText).gameObject.SetActive(true);

        if (matchmakingPanel is not null)
        {
            matchmakingPanel.SetActive(true);
        }

        matchmakingButton = Util.SearchChild<Button>(matchmakingPanel, "MatchmakingButton", false);
        testGameButton = Util.SearchChild<Button>(matchmakingPanel, "TestGameButton", false);
        cancelButton = Util.SearchChild<Button>(matchmakingPanel, "CancelButton", false);

        if (matchmakingButton is not null)
        {
            matchmakingButton.gameObject.BindEvent(StartMatchmaking);
        }

        if (testGameButton is not null)
        {
            testGameButton.gameObject.BindEvent(StartTestGame);
        }

        if (cancelButton is not null)
        {
            cancelButton.gameObject.BindEvent(CancelMatchmaking);
            cancelButton.gameObject.SetActive(false);
        }

        matchmakingButton.gameObject.SetActive(false);
        testGameButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// 매치메이킹 버튼에 추가하는 메치메이킹 Action 델리게이트.
    /// 포톤 서버 상에서 매치 메이킹을 수행합니다.
    /// </summary>
    public void StartMatchmaking(PointerEventData data)
    {
        matchmakingButton.gameObject.SetActive(false);
        testGameButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(true);
        Get<Text>((int)Texts.SearchingText).gameObject.SetActive(true);

        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Searching for a match");
    }

    /// <summary>
    /// 개발용으로 즉시 방을 생성하는 Action 델리게이트.
    /// </summary>
    public void StartTestGame(PointerEventData data)
    {
        RoomOptions devRoomOptions = new RoomOptions();
        devRoomOptions.MaxPlayers = 1;
        devRoomOptions.IsVisible = false;
        int randNum = Random.Range(1, 3000);

        PhotonNetwork.CreateRoom($"DevRoom_{randNum}", devRoomOptions);
    }

    /// <summary>
    /// 매치메이킹을 취소하는 Action 델리게이트.
    /// 방에 들어온 상태면 LeaveRoom을 호출합니다.
    /// </summary>
    public void CancelMatchmaking(PointerEventData data)
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }

        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectUsingSettings();

        Debug.Log("Matchmaking canceled");

        matchmakingButton.gameObject.SetActive(true);
        testGameButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(false);
        Get<Text>((int)Texts.SearchingText).gameObject.SetActive(false);
    }

    /// <summary>
    /// 포톤 클라우드에 연결했을 때 호출되는 콜백입니다.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("Successfully connected to Photon on " + PhotonNetwork.CloudRegion + " Server");
        PhotonNetwork.AutomaticallySyncScene = true;
        GetText((int)Texts.ConnectingText).gameObject.SetActive(false);
        matchmakingPanel.SetActive(true);
        matchmakingButton.gameObject.SetActive(true);
        testGameButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// 플레이어가 룸에 들어오면 (전체 플레이어 아님) 호출되는 콜백입니다.
    /// </summary>
    public override void OnJoinedRoom()
    {
        // 테스트 게임이면 즉시 PreparationScene을 로드합니다.
        if (PhotonNetwork.CurrentRoom.IsVisible == false && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("PreparationScene");
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("There is no random room, try to make a new room");
        int randomRoomName = Random.Range(0, 5000);
        RoomOptions roomOptions =
            new RoomOptions()
            {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = _maxPlayers
            };
        PhotonNetwork.CreateRoom("RoomName_" + randomRoomName, roomOptions);
    }

    // When other Player Enter Room,
    // call this Function and Check if player count equals to maxPlayer of RoomOptions
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        int roomLimit = PhotonNetwork.CurrentRoom.MaxPlayers;
        if (PhotonNetwork.CurrentRoom.PlayerCount == roomLimit && PhotonNetwork.IsMasterClient)
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount + "/2 Starting Game");
            // Start Game
            PhotonNetwork.LoadLevel("PreparationScene");
        }
    }

    /// <summary>
    /// 서버와 연결이 끊겼을 때 호출되는 콜백입니다.
    /// 마스터에 다시 연결해야 하므로 그 전까지 모든 버튼을 비활성화 합니다.
    /// </summary>
    public override void OnDisconnected(DisconnectCause cause)
    {
        matchmakingButton.gameObject.SetActive(false);
        testGameButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        GetText((int)Texts.ConnectingText).gameObject.SetActive(true);
    }

    #region deprecated
    /*
    /// <summary>
    /// 페이스북 인증 기반 로그인을 실시하는 메서드입니다.
    /// </summary>
    public void LoginWithFacebook(PointerEventData data)
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitFBCallBack, OnHideUnity);
        }

        if (FB.IsLoggedIn)
        {
            OnFacebookLoggedIn();
        }
        else
        {
            List<string> perms = new List<string>();
            // perms.Add("gaming_profile");
            // perms.Add("email");
            perms.Add("user_friends");
            perms.Add("public_profile");

            FB.LogInWithReadPermissions(perms, (ILoginResult res) => {
                if (FB.IsLoggedIn)
                {
                    OnFacebookLoggedIn();
                }
                else
                {
                    Debug.LogError($"Error in facebook login {res.Error}");
                }
            });
        }
    }

    /// <summary>
    /// 페이스북에 로그인 되어있을 때 포톤 클라우드에 연결하는 메서드입니다.
    /// 추가로 AuthPanel을 비활성화 하고 MatchmakingPanel을 활성화 합니다.
    /// </summary>
    private void OnFacebookLoggedIn()
    {
        // 포톤 Auth 관련 정보 설정
        string accessToken = AccessToken.CurrentAccessToken.TokenString;
        string facebookId = AccessToken.CurrentAccessToken.UserId;

        Debug.Log($"Authentication complete! {accessToken} {facebookId}");

        PhotonNetwork.AuthValues = new AuthenticationValues();
        PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Facebook;
        PhotonNetwork.AuthValues.UserId = facebookId;
        PhotonNetwork.AuthValues.AddAuthParameter("token", accessToken);
        PhotonNetwork.AuthValues.AddAuthParameter("username", facebookId);

        // Connect to a photon cloud
        PhotonNetwork.ConnectUsingSettings();

        authPanel.SetActive(false);
        matchmakingPanel.SetActive(true);
    }

    /// <summary>
    /// 커스텀 인증을 실패했을 때 호출하는 메서드입니다.
    /// 지금은 페이스북 인증 실패에 관한 오류 로그만 띄웁니다.
    /// </summary>
    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        Debug.LogError($"Error authenticating to Photon using Facebook: {debugMessage}");
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (isGameShown)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    private void InitFBCallBack()
    {
        if (FB.IsInitialized)
        {
            Debug.Log("Facebook SDK initialized");
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to initialize the Fackbook SDK");
        }
    }
    */
    #endregion
}
