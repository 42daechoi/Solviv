using System.Collections;
using System.Collections.Generic;
using Photon.Pun; // Photon 추가
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float sprintSpeed = 14f;
    public float maxVelocityChange = 10f;

    private Vector3 inputDirection; // 이동 방향
    private float currentSpeed; // 현재 속도
    private Rigidbody rb;
    private PhotonView photonView; // PhotonView 추가

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>(); // PhotonView 컴포넌트 가져오기
        currentSpeed = walkSpeed; // 초기 속도 설정
    }

    private void OnEnable()
    {
        // EventManager의 이벤트 구독
        EventManager_Game.Instance.OnPlayerMove += OnPlayerMove;
        EventManager_Game.Instance.OnPlayerSprint += OnPlayerSprint;
    }

    private void OnDisable()
    {
        // EventManager의 이벤트 구독 해제
        if (EventManager_Game.Instance != null)
        {
            EventManager_Game.Instance.OnPlayerMove -= OnPlayerMove;
            EventManager_Game.Instance.OnPlayerSprint -= OnPlayerSprint;
        }
    }

    private void FixedUpdate()
    {
        // 로컬 플레이어만 움직임 처리
        if (photonView.IsMine && inputDirection.magnitude > 0.1f)
        {
            rb.AddForce(CalculateMovement(currentSpeed), ForceMode.VelocityChange);
        }
    }

    private void OnPlayerMove(Vector3 moveDirection)
    {
        inputDirection = moveDirection;
    }

    private void OnPlayerSprint(bool sprinting)
    {
        // 스프린트 상태에 따라 속도 변경
        currentSpeed = sprinting ? sprintSpeed : walkSpeed;
    }

    Vector3 CalculateMovement(float speed)
    {
        Vector3 targetVelocity = new Vector3(inputDirection.x, 0, inputDirection.z);
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= speed;

        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = targetVelocity - velocity;

        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

        velocityChange.y = 0;

        return velocityChange;
    }
}
