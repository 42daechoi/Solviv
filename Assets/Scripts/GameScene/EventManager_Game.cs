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
    public event Action OnEquip;
    public event Action OnDropItem;
    public event Action<int> OnRemoveItem;

    public event Action OnUseItem;
    
    public event Action<Item> OnOpenDoor;
    public event Action<bool> OnUseComputer;

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
        OnEquip?.Invoke();
    }

    public void InvokeDropItem()
    {
        OnDropItem?.Invoke();
    }

    public void InvokeUseItem()
    {
        OnUseItem?.Invoke();
    }
    

    public void InvokeRemoveItem(int slotIndex)
    {
        OnRemoveItem?.Invoke(slotIndex);
    }
    
    public void InvokeOpenDoor(Item usedItem)
    {
        OnOpenDoor?.Invoke(usedItem);
    }

    public void InvokeUseComputer(bool isActComputer)
    {
        OnUseComputer?.Invoke(isActComputer);
    }
}

