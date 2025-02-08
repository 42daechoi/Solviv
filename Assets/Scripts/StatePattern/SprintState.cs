using UnityEngine;

public class SprintState : IState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("Sprint행동 진입");
        player.Animator.SetBool("isSprinting", true);
        Debug.Log("isSprinting 애니메이션 파라미터 설정 완료");
    }

    public void UpdateState(PlayerController player, Vector3 inputDirection, bool isSprinting)
    {
        if (!isSprinting)
        {
            player.TransitionToState(new MoveState());
            return;
        }

        if (inputDirection.magnitude < 0.1f)
        {
            player.TransitionToState(new IdleState());
        }
    }

    public void FixedUpdateState(PlayerController player)
    {
        
        Vector3 movement = new Vector3(player.InputDirection.x, 0, player.InputDirection.z).normalized * player.SpeedSettings.sprintSpeed;
        movement = player.transform.TransformDirection(movement);
        
        player.Rigidbody.MovePosition(player.Rigidbody.position + movement * Time.fixedDeltaTime);
    }

    public void ExitState(PlayerController player)
    {
        Debug.Log("Sprint 행동 벗어남");
        player.Animator.SetBool("isSprinting", false);
    }
    
    public bool CanInteraction()
    {
        return true;
    }

    public bool IsJumping()
    {
        return false;
    }
}