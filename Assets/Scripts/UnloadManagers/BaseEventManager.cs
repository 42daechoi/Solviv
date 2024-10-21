using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseEventManager : MonoBehaviour, IEventManager
{
    public static BaseEventManager Instance { get; private set; }
    
    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public event Action<Vector3> OnPlayerMove;
    public event Action OnJump;

    public virtual void HandleCommonEvents(string keyType, Vector3 moveDirection)
    {
        switch (keyType)
        {
            case "Move":
                InvokePlayerMove(moveDirection);
                break;
            
            case "Jump":
                InvokeJump();
                break;
        }
    }

    public void InvokePlayerMove(Vector3 moveDirection)
    {
        OnPlayerMove?.Invoke(moveDirection);
    }

    public void InvokeJump()
    {
        OnJump?.Invoke();
    }

    public abstract void HandleSpecificEvents();
}
