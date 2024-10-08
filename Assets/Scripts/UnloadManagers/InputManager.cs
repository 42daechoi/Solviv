using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake()
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
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (moveX != 0 || moveZ != 0)
        {
            Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;
            EventManager.Instance.InvokePlayerMove(moveDirection);
        }
        else
        {
            EventManager.Instance.InvokePlayerMove(Vector3.zero);
        }
    }
}
