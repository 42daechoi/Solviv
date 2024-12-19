using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager_Game : MonoBehaviour
{
    public event Action<Vector3> OnPlayerMove;
    public event Action<bool> OnPlayerSprint;

    public static EventManager_Game Instance { get; private set; }

    private void Awake()
    {
        // 싱글톤 패턴 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InvokePlayerMove(Vector3 moveDirection)
    {
        OnPlayerMove?.Invoke(moveDirection);
    }

    public void InvokePlayerSprint(bool sprinting)
    {
        OnPlayerSprint?.Invoke(sprinting);
    }
}