using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public event Action<Vector3> PlayerMove;

    // Update is called once per frame
    void Awake()
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

    public void InvokePlayerMove(Vector3 moveDirection)
    {
        PlayerMove?.Invoke(moveDirection);
    }
}
