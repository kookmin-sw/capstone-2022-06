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
    private byte _maxPlayers = 4;
    
    enum GameObjects
    {
        MatchmakingPanel,
    }

    enum Texts
    {
        SearchingText,
        ConnectingText
    }

    enum Buttons
    {
        QuitButton
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
        Bind<Button>(typeof(Buttons));
    
        matchmakingPanel = Get<GameObject>((int)GameObjects.MatchmakingPanel);
        Get<Text>((int)Texts.SearchingText).gameObject.SetActive(false);
        GetText((int)Texts.ConnectingText).gameObject.SetActive(true);

        if (matchmakingPanel is not null)
        {
            matchmakingPanel.SetActive(true);
        }

        GetButton((int)Buttons.QuitButton).onClick.AddListener(() => {
            Application.Quit();
        });

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
    /// ??????????????? ????????? ???????????? ??????????????? Action ???????????????.
    /// ?????? ?????? ????????? ?????? ???????????? ???????????????.
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
    /// ??????????????? ?????? ?????? ???????????? Action ???????????????.
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
    /// ?????????????????? ???????????? Action ???????????????.
    /// ?????? ????????? ????????? LeaveRoom??? ???????????????.
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
    /// ?????? ??????????????? ???????????? ??? ???????????? ???????????????.
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
    /// ??????????????? ?????? ???????????? (?????? ???????????? ??????) ???????????? ???????????????.
    /// </summary>
    public override void OnJoinedRoom()
    {
        // ????????? ???????????? ?????? PreparationScene??? ???????????????.
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
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel("PreparationScene");
        }
    }

    /// <summary>
    /// ????????? ????????? ????????? ??? ???????????? ???????????????.
    /// ???????????? ?????? ???????????? ????????? ??? ????????? ?????? ????????? ???????????? ?????????.
    /// </summary>
    public override void OnDisconnected(DisconnectCause cause)
    {
        matchmakingButton.gameObject.SetActive(false);
        testGameButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        GetText((int)Texts.ConnectingText).gameObject.SetActive(true);
    }
}
