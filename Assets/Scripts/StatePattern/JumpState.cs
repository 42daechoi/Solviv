using UnityEngine;

public class JumpState : IState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("Jump 상태 진입");
        
        Vector3 jumpForce = new Vector3(0, player.SpeedSettings.jumpForce, 0);
        player.Rigidbody.AddForce(jumpForce, ForceMode.Impulse);
        
    }

    public void UpdateState(PlayerController player, Vector3 inputDirection, float offset)
    {
        
    }

    public void FixedUpdateState(PlayerController player)
    {
        if (player.IsGrounded() && player.Rigidbody.velocity.y <= 0.1f)
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
    
    
}