using System.Collections;
using System.Collections.Generic;
using Photon.Pun; // Photon 추가
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float maxVelocityChange = 10f;

    private Vector2 input;
    private Rigidbody rb;
    private PhotonView photonView; // PhotonView 추가

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>(); // PhotonView 컴포넌트 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        // 로컬 플레이어만 입력 처리
        if (photonView.IsMine)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            input.Normalize();
        }
    }

    private void FixedUpdate()
    {
        // 로컬 플레이어만 움직임 처리
        if (photonView.IsMine)
        {
            rb.AddForce(CalculateMovement(walkSpeed), ForceMode.VelocityChange);
        }
    }

    Vector3 CalculateMovement(float _speed)
    {
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= _speed;

        Vector3 velocity = rb.velocity;
        if (input.magnitude > 0.5f)
        {
            Vector3 velocityChange = targetVelocity - velocity;

            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

            velocityChange.y = 0;

            return velocityChange;
        }
        else
        {
            return Vector3.zero;
        }
    }
}