using Photon.Pun;
using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Realtime;
using System;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static event Action<bool> OnChangedReady;

    public override void OnEnable()
    {
        base.OnEnable();
        InputManager_Lobby.OnPlayerReady += OnChangeReadyButtonClick;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        InputManager_Lobby.OnPlayerReady -= OnChangeReadyButtonClick;
    }

    public override void OnJoinedRoom()
    {
        // 방에 들어오면 isReady 프로퍼티를 기본값으로 false로 설정
        if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("isReady"))
        {
            Hashtable props = new Hashtable
            {
                { "isReady", false }
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }
    }

    public void OnChangeReadyButtonClick()
    {
        bool currState = PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("isReady")
                         && (bool)PhotonNetwork.LocalPlayer.CustomProperties["isReady"];
        SetReady(!currState);
    }

    private void SetReady(bool currState)
    {
        Hashtable props = new Hashtable
        {
            { "isReady", currState }
        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        // 플레이어 프로퍼티 업데이트 콜백 함수
        if (targetPlayer == PhotonNetwork.LocalPlayer && changedProps.ContainsKey("isReady"))
        {
            bool currState = (bool)targetPlayer.CustomProperties["isReady"];
            OnChangedReady?.Invoke(currState);
        }
        ReadyCheckAndStartGame();
    }

    private void ReadyCheckAndStartGame()
    {
        if (PhotonNetwork.IsMasterClient && IsAllReady())
        {
            StartGame();
        }
    }

    private bool IsAllReady()
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (!player.CustomProperties.ContainsKey("isReady") || !(bool)player.CustomProperties["isReady"])
            {
                return false;
            }
        }
        return true;
    }

    private void StartGame()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }
}
