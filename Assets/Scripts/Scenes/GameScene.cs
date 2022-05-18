using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using photonHash = ExitGames.Client.Photon.Hashtable;

public class GameScene : BaseScene
{
    Vector3 spawnPoint = Vector3.zero;
    bool isCommander;
    int myId;
    string myPrefabPath;

    GameObject myChamp;

    void Start()
    {
        isCommander = (bool)GetPropVal("isCommander");
        myId = (int)GetPropVal("actorId");
        myPrefabPath = (string)GetPropVal("prefabPath");
        // PrintMyProp();

        // 초기 스폰 좌표 지정. 지휘관일 경우 초기 view 위치를 지정할 예정
        // Blue Team
        if (Util.GetMyLayerString() == "BlueTeam")
        {
            spawnPoint.x = Random.Range(-111, -98);
            spawnPoint.z = Random.Range(-111, -98);
        }
        else // Red Team
        {
            spawnPoint.x = Random.Range(98, 111);
            spawnPoint.z = Random.Range(98, 111);
        }

        if (!isCommander)
        {
            Spawn();
        }
        else
        {
            // 지휘관이면 CameraFollow를 제거하고 CommanderCamController를 부착합니다.
            // 스폰 지점으로 이동시킵니다.
            CameraFollow tracker = Camera.main.gameObject.GetOrAddComponent<CameraFollow>();

            if (tracker)
            {
                Destroy(tracker);
            }

            Vector3 origin = Camera.main.transform.position;
            origin.x = spawnPoint.x;
            origin.z = spawnPoint.z;
            Camera.main.transform.position = origin;

            Camera.main.gameObject.GetOrAddComponent<CommanderCamController>();
        }
    }

    protected override void Init()
    {
        base.Init();

        sceneType = Define.Scene.Game;
        Managers.UI.ShowSceneUI<UI_GameScene>();
        
        // 게임 시작 시간을 커스텀 프로퍼티로 저장합니다.
        // 마스터만 저장하도록 강제합니다.
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new photonHash() {
                {"startTimestamp", PhotonNetwork.Time}
            });
        }
    }

    public override void Clear() {}

    private void PrintMyProp()
    {
        Debug.Log($"Status of commander {isCommander}");
        Debug.Log($"Status of local id {myId}");
        Debug.Log(myPrefabPath);
    }

    /// <summary>
    /// 코드를 줄이기 위해 CustomProperties에서 값을 가져오는 코드를 메서드로 감쌉니다.
    /// </summary>
    object GetPropVal(object key)
    {
        object ret;

        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(key, out ret))
        {
            return ret;
        }

        return null;
    }

    /// <summary>
    /// 지휘관 포함 스폰할 때 일어나는 동작입니다.
    /// </summary>
    void Spawn()
    {
        // 지휘관이 아니면 프리팹을 경로로 받아 챔피언을 스폰
        // 카메라를 따라가게 하는 컴포넌트인 CameraFollow를 부착합니다.
        CameraFollow tracker = Camera.main.gameObject.GetOrAddComponent<CameraFollow>();

        myChamp = PhotonNetwork.Instantiate(myPrefabPath, spawnPoint, Quaternion.identity);
        tracker.player = myChamp.transform;
        Camera.main.gameObject.GetComponent<InputTargeting>().Initialize(myChamp);
        LayerController _layer = myChamp.GetOrAddComponent<LayerController>();
        _layer.SetLayer(Util.GetMyLayerString());
    }
}
