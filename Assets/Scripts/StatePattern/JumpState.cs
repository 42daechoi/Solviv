using UnityEngine;

public class JumpState : IState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("Jump 상태 진입");

        // 점프 애니메이션 설정 및 힘 적용
        Vector3 jumpForce = new Vector3(0, player.SpeedSettings.jumpForce, 0);
        player.Rigidbody.AddForce(jumpForce, ForceMode.Impulse);
        
    }

    public void UpdateState(PlayerController player, Vector3 inputDirection, bool isSprinting)
    {
        
    }

    public void FixedUpdateState(PlayerController player)
    {
        if (player.IsGrounded())
        {
            player.TransitionToState(new IdleState());
        }
    }

    public void ExitState(PlayerController player)
    {
        Debug.Log("Jump 상태 종료");
    }

    public bool CanInteraction()
    {
        return false;  // 점프 중에는 상호작용 불가
    }

    public bool IsJumping()
    {
        return true;
    }
}