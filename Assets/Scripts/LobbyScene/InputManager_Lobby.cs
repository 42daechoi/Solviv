using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_Lobby : MonoBehaviour
{
    public static event Action OnPlayerReady;
    public static event Action OnTryPickUpWeapon;

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
