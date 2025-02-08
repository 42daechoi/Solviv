using UnityEngine;

public class MoveState : IState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("Move행동 진입");
        player.Animator.SetBool("isWalking", true);
    }

    public void UpdateState(PlayerController player, Vector3 inputDirection, bool isSprinting)
    {
        if (isSprinting)
        {
            player.TransitionToState(new SprintState());
            return;
        }

        if (inputDirection.magnitude < 0.1f)
        {
            player.TransitionToState(new IdleState());
        }
    }

    public void FixedUpdateState(PlayerController player)
    {
        Vector3 movement = new Vector3(player.InputDirection.x, 0, player.InputDirection.z).normalized * player.SpeedSettings.walkSpeed;

        // 월드 좌표로 변환
        movement = player.transform.TransformDirection(movement);

        // MovePosition으로 이동
        player.Rigidbody.MovePosition(player.Rigidbody.position + movement * Time.fixedDeltaTime);

    }

    public void ExitState(PlayerController player)
    {
        Debug.Log("Move행동 벗어남");
        player.Animator?.SetBool("isWalking", false);
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