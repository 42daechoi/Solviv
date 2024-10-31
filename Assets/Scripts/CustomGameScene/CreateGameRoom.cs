using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using ExitGames.Client.Photon;

public class RoomManager : MonoBehaviourPunCallbacks
{
    // UI 요소
    public Toggle publicToggle;
    public Toggle privateToggle;
    public Toggle soloModeToggle;
    public Toggle multiModeToggle;
    public TMP_InputField numberOfPeopleInput;
    public Button confirmButton;

    private void Start()
    {
        // Confirm 버튼 클릭 시 방 생성 함수 호출
        confirmButton.onClick.AddListener(CreateRoom);
    }

    

    // 방 생성 함수
    private void CreateRoom()
{
    // 방 옵션 설정
    RoomOptions roomOptions = new RoomOptions();

    // Public/Private 설정
    if (publicToggle.isOn)
    {
        roomOptions.IsVisible = true; // 공개된 방
        roomOptions.IsOpen = true;    // 누구나 들어올 수 있음
    }
    else if (privateToggle.isOn)
    {
        roomOptions.IsVisible = false; // 비공개 방
        roomOptions.IsOpen = true;     // 초대 받은 사람만 들어올 수 있음
    }

    // 최대 인원 설정
    int maxPlayers;
    if (int.TryParse(numberOfPeopleInput.text, out maxPlayers))
    {
        roomOptions.MaxPlayers = (byte)maxPlayers;
    }

    // Custom Properties 설정 (게임 모드 추가)
    Hashtable customProperties = new Hashtable();
    string selectedGameMode = soloModeToggle.isOn ? "SoloMode" : "MultiMode";
    customProperties.Add("GameMode", selectedGameMode);  // SoloMode 또는 MultiMode 설정
    roomOptions.CustomRoomProperties = customProperties;

    // 로비에서 표시할 Custom Properties 키 설정
    roomOptions.CustomRoomPropertiesForLobby = new string[] { "GameMode" };

    // 방 이름 및 생성
    string roomName = "Room_" + Random.Range(1000, 10000); // 임의로 방 이름 설정
    PhotonNetwork.CreateRoom(roomName, roomOptions);
    
    Debug.Log($"방 생성됨: {roomName}, 게임 모드: {selectedGameMode}, 최대 인원: {maxPlayers}");
}

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"방 생성 실패: {message}");
    }

    // 포톤 서버에 연결 실패한 경우 처리
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError($"포톤 서버 연결 실패: {cause}");
    }
}