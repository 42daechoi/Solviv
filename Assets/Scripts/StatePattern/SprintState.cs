using UnityEngine;

public class SprintState : IMovementState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("Entered Sprint State");
    }

    public void UpdateState(PlayerController player)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        player.inputDirection = new Vector3(horizontal, 0, vertical).normalized;
        
        if (player.inputDirection.magnitude < 0.1f)
        {
            player.TransitionToState(new IdleState());
            return;
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            player.TransitionToState(new MoveState());
        }
    }

    public void FixedUpdateState(PlayerController player)
    {
        if (player.inputDirection.magnitude > 0.1f)
        {
            Vector3 targetVelocity = player.CalculateMovement(player.SpeedSettings.sprintSpeed);
            player.Rigidbody.velocity = Vector3.Lerp(player.Rigidbody.velocity, targetVelocity, player.SpeedSettings.smoothSpeed);
        }
    }

    public void ExitState(PlayerController player)
    {
        Debug.Log("Exiting Sprint State");
    }
}