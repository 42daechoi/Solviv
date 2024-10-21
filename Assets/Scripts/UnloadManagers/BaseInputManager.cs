using System;
using UnityEngine;

public abstract class BaseInputManager : MonoBehaviour, IInputManager
{
    
    public static BaseInputManager Instance { get; private set; }
    
    protected virtual void Awake()
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
        HandleCommonInput();
        HandleSpecificInput();
    }

    public virtual void HandleCommonInput() //공용 Input
    {
        MoveInput();
        JumpInput();
    }
    
    protected void MoveInput()
    {
        float hzInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(hzInput, 0, vInput);

        if (moveDirection != Vector3.zero)
        {
            BaseEventManager.Instance.HandleCommonEvents("Move", moveDirection);
        }
    }
    
    protected void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BaseEventManager.Instance.HandleCommonEvents("Jump", Vector3.zero);
        }
    }
    
    

    public abstract void HandleSpecificInput(); // 각 씬마다 차이나는 Input
}