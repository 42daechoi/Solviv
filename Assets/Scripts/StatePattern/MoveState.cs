using UnityEngine;

public class MoveState : IState
{

    public void EnterState(PlayerController player)
    {
        Debug.Log("Move행동 진입");
    }

    public void UpdateState(PlayerController player)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        player.inputDirection = new Vector3(horizontal, 0, vertical).normalized;
        
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
            // 기본 속도인 WalkSpeed와 smoothSpeed의 보간값을 넣어 자연스럽게 줄어들게만듬
            Vector3 targetVelocity = player.CalculateMovement(player.SpeedSettings.walkSpeed);
            player.Rigidbody.velocity = Vector3.Lerp(player.Rigidbody.velocity, targetVelocity, player.SpeedSettings.smoothSpeed);
        }
    }

    public void ExitState(PlayerController player)
    {
        Debug.Log("Move행동 벗어남");
    }
    
    public bool CanInteraction()
    {
        return false;
    }
}