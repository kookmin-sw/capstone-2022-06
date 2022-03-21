using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject findMatchBtn;
    [SerializeField] GameObject searchingPanel;

    void Start()
    {
        searchingPanel.SetActive(false);
        findMatchBtn.SetActive(false);

        DontDestroyOnLoad(gameObject);

        // Connect to the photon Server
        PhotonNetwork.ConnectUsingSettings();   
    }

    // callback by ConnectUsingSettings called in Start 
    public override void OnConnectedToMaster()
    {
        Debug.Log("We are Connected to Photon! on" + PhotonNetwork.CloudRegion + "Server");
        PhotonNetwork.AutomaticallySyncScene = true;
        findMatchBtn.SetActive(true);
    }

    // called by clicking "FindMatch" Button
    public void FindMatch()
    {
        searchingPanel.SetActive(true);
        findMatchBtn.SetActive(false);

        PhotonNetwork.JoinRandomRoom();     // Find Random Room Function
        Debug.Log("Searching for a Game");
    }

    // if fail to join random room because there is no room,
    // callback this function and create Room
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Couldn't Find Room - Creating a Room");
        MakeRoom();
    }

    // Create Room Function
    void MakeRoom()
    {
        int randomRoomName = Random.Range(0, 5000);
        RoomOptions roomOptions =
            new RoomOptions()
            {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = 2
            };
        PhotonNetwork.CreateRoom("RoomName_" + randomRoomName, roomOptions);
        Debug.Log("Room Created, Waiting For Another Player");
    }

    // When other Player Enter Room,
    // call this Function and Check if player count equals to maxPlayer of RoomOptions
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount + "/2 Starting Game");
            // Start Game
            PhotonNetwork.LoadLevel(1);     // 1 is SceneIndex
        }
    }

    // Call by Clicking "Stop Searching" Button and Leave Room
    public void StopSearch()
    {
        searchingPanel.SetActive(false);
        findMatchBtn.SetActive(true);
        PhotonNetwork.LeaveRoom();
        Debug.Log("Stopped, Back to Menu");
    }
}
