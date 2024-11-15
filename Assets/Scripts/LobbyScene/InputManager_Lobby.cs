using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class InputManager_Lobby : MonoBehaviourPun
{
    public static event Action OnPlayerReady;
    public static event Action OnTryPickUpWeapon;
    public static event Action OnPlayerShoot;
    public static event Action OnPlayerReload;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpdatePlayerReady();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryPickUpWeapon();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            OnPlayerShoot?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnPlayerReload?.Invoke();
        }
    }

    private void UpdatePlayerReady()
    {
        OnPlayerReady?.Invoke();
    }

    private void TryPickUpWeapon()
    {
        OnTryPickUpWeapon?.Invoke();
    }
}
