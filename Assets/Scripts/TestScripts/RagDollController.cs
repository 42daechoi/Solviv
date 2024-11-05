using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollController : MonoBehaviour
{
    public float speed;
    public float strafeSpeed;
    public float jumpForce;

    public Rigidbody hips;
    public Rigidbody leftFoot;
    public Rigidbody rightFoot;
    
    public float liftForce = 5f;     // 발을 들어 올리는 힘
    public float pushForce = 20f;    // 앞으로 밀어내는 힘
    public float backwardPushForce = 10f; // 뒤로 밀어내는 힘
    public float stepInterval = 0.5f; // 걸음 주기
    public float brakingForce = 0.8f; // 멈출 때 브레이킹 강도

    public bool isGrounded;
    
    void Start()
    {
        hips = GetComponent<Rigidbody>();
        StartCoroutine(WalkCycle());
    }

    private void FixedUpdate()
    {
        // 이동 입력 처리
        bool isMoving = false;
        
        if (Input.GetKey(KeyCode.W))
        {
            isMoving = true;
            hips.AddForce(hips.transform.forward * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            isMoving = true;
            hips.AddForce(-hips.transform.forward * (speed * 0.5f));
        }

        if (Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            hips.AddForce(-hips.transform.right * strafeSpeed);    
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            hips.AddForce(hips.transform.right * strafeSpeed);    
        }

        // 점프 입력
        if (Input.GetAxis("Jump") > 0 && isGrounded)
        {
            hips.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // 브레이킹 적용 - 이동이 멈출 때 속도 감속
        if (!isMoving)
        {
            hips.velocity = new Vector3(hips.velocity.x * brakingForce, hips.velocity.y, hips.velocity.z * brakingForce);
        }
    }
    

    IEnumerator WalkCycle()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.W)) // 앞으로 걷기 동작
            {
                LiftAndPushFoot(leftFoot, hips.transform.forward, pushForce);
                yield return new WaitForSeconds(stepInterval);

                LiftAndPushFoot(rightFoot, hips.transform.forward, pushForce);
                yield return new WaitForSeconds(stepInterval);
            }
            else if (Input.GetKey(KeyCode.S)) // 뒤로 걷기 동작
            {
                LiftAndPushFoot(leftFoot, -hips.transform.forward, backwardPushForce);
                yield return new WaitForSeconds(stepInterval);

                LiftAndPushFoot(rightFoot, -hips.transform.forward, backwardPushForce);
                yield return new WaitForSeconds(stepInterval);
            }
            else
            {
                yield return null;
            }
        }
    }

    void LiftAndPushFoot(Rigidbody foot, Vector3 direction, float force)
    {
        foot.AddForce(Vector3.up * liftForce, ForceMode.Impulse);
        StartCoroutine(PushAfterLift(foot, direction, force));
    }

    IEnumerator PushAfterLift(Rigidbody foot, Vector3 direction, float force)
    {
        yield return new WaitForSeconds(0.1f);
        foot.AddForce(direction * force, ForceMode.Impulse);
    }
}
