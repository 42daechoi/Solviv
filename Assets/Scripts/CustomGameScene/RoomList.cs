using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomList : MonoBehaviourPunCallbacks
{
    [Header("UI")] 
    public Transform roomListParent;
    public GameObject roomListItemPrefab;

    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    IEnumerator Start()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }

        yield return new WaitUntil(() => !PhotonNetwork.InRoom);

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList)
            {
                // 리스트에서 제거된 방은 삭제
                cachedRoomList.Remove(roomInfo.Name);
            }
            else
            {
                // 새 방 추가 또는 기존 방 정보 업데이트
                cachedRoomList[roomInfo.Name] = roomInfo;
            }
        }

        UpdateUI();
    }

    void UpdateUI()
{
    // 기존 룸 리스트 아이템 제거
    foreach (Transform roomItem in roomListParent)
    {
        Destroy(roomItem.gameObject);
    }

    // 룸 리스트 재생성
    foreach (var roomEntry in cachedRoomList)
    {
        RoomInfo room = roomEntry.Value;

        GameObject roomItem = Instantiate(roomListItemPrefab, roomListParent);

        // 방 이름
        roomItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name;

        // 방의 게임 모드 표시
        if (room.CustomProperties.ContainsKey("GameMode"))
        {
            string gameMode = (string)room.CustomProperties["GameMode"];
            roomItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = gameMode;  // 두 번째 항목에 게임 모드 표시
        }

        // 플레이어 수 / 최대 플레이어 수 표시
        roomItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = room.PlayerCount + "/" + room.MaxPlayers;
    }
}
}
