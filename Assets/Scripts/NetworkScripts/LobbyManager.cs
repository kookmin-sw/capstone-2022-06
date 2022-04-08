using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Facebook.Unity;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject findMatchBtn;
    [SerializeField] GameObject searchingPanel;

    void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() => {
                if (!FB.IsInitialized)
                {
                    Debug.Log("Failed to initialize the Fackbook SDK");
                }
            });
        }
    }

    void Start()
    {
        searchingPanel.SetActive(false);
        findMatchBtn.SetActive(false);

        DontDestroyOnLoad(gameObject);

        // Connect to the photon Server
        PhotonNetwork.ConnectUsingSettings();   
    }

    // callback by ConnectUsingSettings called in Start 
    /// <summary>
    /// 포톤 클라우드에 연결했을 때 호출되는 메서드입니다.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("Successfully connected to Photon on" + PhotonNetwork.CloudRegion + "Server");
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

    /// <summary>
    /// 페이스북 인증 기반 로그인을 실시하는 메서드입니다.
    /// </summary>
    private void LoginWithFacebook()
    {
        if (FB.IsLoggedIn)
        {
            OnFacebookLoggedIn();
        }
        else
        {
            List<string> perms = new List<string>();
            perms.Add("public_profile");
            perms.Add("email");
            perms.Add("user_friends");

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
    /// </summary>
    private void OnFacebookLoggedIn()
    {
        // Auth 관련 정보 설정
        string accessToken = AccessToken.CurrentAccessToken.TokenString;
        string facebookId = AccessToken.CurrentAccessToken.UserId;
        PhotonNetwork.AuthValues = new AuthenticationValues();
        PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Facebook;
        PhotonNetwork.AuthValues.UserId = facebookId;
        PhotonNetwork.AuthValues.AddAuthParameter("token", accessToken);

        // Connect to a photon server
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// 커스텀 인증을 실패했을 때 호출하는 메서드입니다.
    /// 지금은 페이스북 인증 실패에 관한 오류 로그만 띄웁니다.
    /// </summray>
    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        Debug.LogError($"Error authenticating to Photon using Facebook: {debugMessage}");
    }
}
