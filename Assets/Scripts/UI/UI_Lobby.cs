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
    
    enum GameObjects
    {
        MatchmakingPanel,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
    
        matchmakingPanel = Get<GameObject>((int)GameObjects.MatchmakingPanel);
        matchmakingPanel.SetActive(true);

        PhotonNetwork.ConnectUsingSettings();   
    }

    public void StartMatchmaking(PointerEventData data)
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public void JoinTestField(PointerEventData data)
    {
        RoomOptions devRoomOptions = new RoomOptions();
        devRoomOptions.MaxPlayers = 1;
        devRoomOptions.IsVisible = false;

        PhotonNetwork.CreateRoom("DevRoom", devRoomOptions);
    }

    public void CancelMatchmaking(PointerEventData data)
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    /// <summary>
    /// 포톤 클라우드에 연결했을 때 호출되는 메서드입니다.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("Successfully connected to Photon on" + PhotonNetwork.CloudRegion + "Server");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        PhotonNetwork.LoadLevel("GameScene");
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
