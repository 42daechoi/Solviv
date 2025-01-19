using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager_Game : MonoBehaviour
{
    public event Action<Vector3> OnPlayerMove;
    public event Action<bool> OnPlayerSprint;
    public event Action OnInteraction;
    public event Action<int> OnHeldItem;
    public event Action OnDropItem;

    public event Action OnUseItem;

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

    // 스프린트 이벤트 발행
    public void InvokeSprint(bool isSprint)
    {
        OnPlayerSprint?.Invoke(isSprint);
    }
    public void InvokeInteraction()
    {
        OnInteraction?.Invoke();
    }
    public void InvokeHeldItem(int keyCode)
    {
        OnHeldItem?.Invoke(keyCode);
    }

    public void InvokeDropItem()
    {
        OnDropItem?.Invoke();
    }

    public void InvokeUseItem()
    {
        OnUseItem?.Invoke();
    }
}

// IdleState 내부에 Interaction 구현하면은 = 구조 단순화 되면서 확장성은 x
// ㄴ 책임분리가 안됨

// PC, Interaction 관리하면 구조는 if문써서 연산은 추가될진몰라도 확장성은 기가막히다.
// ㄴ 책임분리 o

// MoveState 말그대로