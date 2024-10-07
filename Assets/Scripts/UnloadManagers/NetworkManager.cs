using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        // ���� ���� ����
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("���� ������ ���� ���� �Ϸ�");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� ���� �Ϸ�");
        // �� ���� �Ǵ� ���忡 �ʿ��� �غ� �ڵ� �ʿ�
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 4 };
        PhotonNetwork.CreateRoom("RoomName", roomOptions);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("�� ���� �Ϸ�");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("RoomName");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("�� ���� �Ϸ�");
        // �÷��̾� ĳ���� �ν��Ͻ�ȭ ���� �ʿ�
        // PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity, 0);
    }

    //public override void OnPlayerLeftRoom(Player otherPlayer)
    //{
    //    Debug.Log(otherPlayer.NickName + "���� �����ϼ̽��ϴ�.");
    //}
    [PunRPC]
    public void YourRpcMethod(string message)
    {
        Debug.Log("Received RPC message: " + message);
    }

    // �ٸ� �޼��忡�� RPC ȣ��
    public void CallRpc()
    {
        photonView.RPC("YourRpcMethod", RpcTarget.All, "Hello, everyone!");
    }
}
