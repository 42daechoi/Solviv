using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager_Game : MonoBehaviour
{
    public event Action<Vector3> OnPlayerMove;
    
    public static EventManager_Game Instance { get; private set; }
    
    private void Awake()
    {
        // 싱글톤 패턴을 위한 설정
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
        if (OnPlayerMove != null)
        {
            OnPlayerMove.Invoke(moveDirection);
        }
    }
}
