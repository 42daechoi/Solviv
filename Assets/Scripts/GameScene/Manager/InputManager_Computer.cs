using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_Computer : MonoBehaviour
{
    private PlayerController _playerController;

    void Start()
    {
        StartCoroutine(WaitForPlayerController());
    }

    private IEnumerator WaitForPlayerController()
    {
        while (_playerController == null)
        {
            _playerController = PlayerController.Instance;
            if (_playerController == null)
            {
                Debug.Log("PlayerController가 아직 초기화되지 않았습니다. 대기 중...");
                yield return new WaitForSeconds(0.1f);
            }
        }
        Debug.Log("PlayerController 초기화 완료!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            EventManager_Game.Instance.InvokeUseComputer(false);
        }
    }
}
