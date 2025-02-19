using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_Computer : MonoBehaviour
{
    private PlayerController _playerController;

    void Start()
    {
        _playerController = PlayerController.LocalPlayerInstance;

        if (_playerController == null)
        {
            Debug.LogError("PlayerController 인스턴스를 찾을 수 없습니다.");
            return;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            EventManager_Game.Instance.InvokeUseComputer(false);
        }
    }
}
