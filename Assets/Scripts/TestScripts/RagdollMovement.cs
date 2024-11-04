using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollMovement : MonoBehaviour
{
    public GameObject targetObject;  // 인스펙터에서 할당할 이동할 오브젝트
    public float moveSpeed = 5f;     // 이동 속도

    private Transform targetTransform;
    
    void Start()
    {
        // 할당된 타겟 오브젝트가 있는지 확인
        if (targetObject != null)
        {
            targetTransform = targetObject.transform;
        }
        else
        {
            Debug.LogError("Target object is not assigned in the inspector!");
        }
    }

    void Update()
    {
        // 타겟 오브젝트가 설정되어 있을 때만 이동 실행
        if (targetTransform != null)
        {
            // WASD 입력을 받습니다.
            float moveHorizontal = Input.GetAxis("Horizontal");  // A와 D 키
            float moveVertical = Input.GetAxis("Vertical");      // W와 S 키

            // 이동 방향 벡터를 생성합니다.
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            // 이동 속도를 곱하여 매 프레임마다 이동
            targetTransform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
