using UnityEngine;

public class IdleState : IMovementState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("Entered Idle State");
    }

    public void UpdateState(PlayerController player)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        player.inputDirection = new Vector3(horizontal, 0, vertical);

        if (player.inputDirection.magnitude > 0.1f)
        {
            player.TransitionToState(new MoveState());
        }
    }
    
    public void FixedUpdateState(PlayerController player)
    {
        // 필요하지 않음
    }

    public void ExitState(PlayerController player)
    {
        Debug.Log("Exiting Idle State");
    }
}