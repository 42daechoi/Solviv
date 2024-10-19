using System;
using UnityEngine;

public abstract class BaseEventManager : MonoBehaviour, IEventManager
{
    public static BaseEventManager Instance { get; private set; }

    public event Action<Vector3> OnPlayerMove;
    
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
    
    protected void Update()
    {
        HandleCommonEvents();
        HandleSpecificEvents();
    }
    

    public virtual void HandleCommonEvents()
    {
        if (OnPlayerMove != null)
        {
            //OnPlayerMove.Invoke();
        }
    }
    public abstract void HandleSpecificEvents();
}
