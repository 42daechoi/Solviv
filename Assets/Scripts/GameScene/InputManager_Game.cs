using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_Game : MonoBehaviour
{
    void Update()
    {
        float hzInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(hzInput, 0, vInput);
        if (moveDirection != Vector3.zero)
        {
            EventManager_Game.Instance.InvokePlayerMove(moveDirection);
        }
    }
    
}
