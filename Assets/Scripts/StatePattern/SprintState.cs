using UnityEngine;

public class SprintState : IState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("Sprint행동 진입");
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
        Vector3 targetVelocity = player.CalculateMovement(player.SpeedSettings.sprintSpeed);
        player.Rigidbody.velocity = Vector3.Lerp(player.Rigidbody.velocity, targetVelocity, player.SpeedSettings.smoothSpeed);
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