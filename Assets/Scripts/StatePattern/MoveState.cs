using UnityEngine;

public class MoveState : IMovementState
{

    public void EnterState(PlayerController player)
    {
        Debug.Log("Entered Move State");
    }

    public void UpdateState(PlayerController player)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        player.inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        // 스프린트 키가 눌리면 Sprint 상태로 전환
        if (Input.GetKey(KeyCode.LeftShift))
        {
            player.TransitionToState(new SprintState());
            return;
        }

        // 스프린트 종료 조건: 이동 방향이 없으면 Idle 상태로 전환
        if (player.inputDirection.magnitude < 0.1f)
        {
            player.TransitionToState(new IdleState());
        }
    }

    public void FixedUpdateState(PlayerController player)
    {
        if (player.inputDirection.magnitude > 0.1f)
        {
            // 기본 속도인 WalkSpeed로 부드럽게 이동
            Vector3 targetVelocity = player.CalculateMovement(player.SpeedSettings.walkSpeed);
            player.Rigidbody.velocity = Vector3.Lerp(player.Rigidbody.velocity, targetVelocity, player.SpeedSettings.smoothSpeed);
        }
    }

    public void ExitState(PlayerController player)
    {
        Debug.Log("Exiting Move State");
    }
}