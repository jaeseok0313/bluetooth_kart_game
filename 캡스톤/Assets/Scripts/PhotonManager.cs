using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "v1.0";
    private string userId = "Ojui";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

    }
    private void Start()
    {
        Debug.Log("00.포매시작");
        PhotonNetwork.NickName = userId;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("01.포서 접속");
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short retrunCode, string message)
    {
        Debug.Log("022. 랜덤 룸 접근 실패");
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 30;
        PhotonNetwork.CreateRoom("room_1", ro);

    }
    public override void OnCreatedRoom()
    {
        Debug.Log("03.방 생성 완료");

    }
    public override void OnJoinedRoom()
    {
        Debug.Log("04.방 입장 완료");
        LobbyManager.instance.isConnect = true;
    }


}