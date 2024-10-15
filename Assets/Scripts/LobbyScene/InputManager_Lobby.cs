using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_Lobby : MonoBehaviour
{
    public static event Action OnPlayerReady;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnPlayerReady?.Invoke();
        }
    }
}
