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

    public void OnChangeReadyButtonClick()
    {
        bool currState = (bool)PhotonNetwork.LocalPlayer.CustomProperties["isReady"];
        SetReady(!currState);
    }

    private void SetReady(bool ready)
    {
        Hashtable props = new Hashtable
        {
            { "isReady", ready }
        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        bool currState = (bool)PhotonNetwork.LocalPlayer.CustomProperties["isReady"];
        OnChangedReady?.Invoke(currState);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey("isReady"))
        {
            ReadyCheckAndStartGame();
        }
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
