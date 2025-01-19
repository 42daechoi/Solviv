using UnityEngine;

public class MoveState : IState
{

    public void EnterState(PlayerController player)
    {
        Debug.Log("Move행동 진입");
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
        Vector3 targetVelocity = player.CalculateMovement(player.SpeedSettings.walkSpeed);
        player.Rigidbody.velocity = Vector3.Lerp(player.Rigidbody.velocity, targetVelocity, player.SpeedSettings.smoothSpeed);
    }

    public void ExitState(PlayerController player)
    {
        Debug.Log("Move행동 벗어남");
    }
    
    public bool CanInteraction()
    {
        return true;
    }
}