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
        Debug.Log("00.���Ž���");
        PhotonNetwork.NickName = userId;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("01.���� ����");
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short retrunCode, string message)
    {
        Debug.Log("022. ���� �� ���� ����");
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 30;
        PhotonNetwork.CreateRoom("room_1", ro);

    }
    public override void OnCreatedRoom()
    {
        Debug.Log("03.�� ���� �Ϸ�");

    }
    public override void OnJoinedRoom()
    {
        Debug.Log("04.�� ���� �Ϸ�");
        LobbyManager.instance.isConnect = true;
    }


}