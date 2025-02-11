using UnityEngine;

public class SprintState : IState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("Sprint행동 진입");
        Debug.Log("isSprinting 애니메이션 파라미터 설정 완료");
    }

    public void UpdateState(PlayerController player, Vector3 inputDirection, float offset)
    {
        if (offset <= 0.5f)
        {
            player.TransitionToState(new MoveState());
        }
        player.UpdateAnimator();
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
    }
    
    public bool CanInteraction()
    {
        return true;
    }
}