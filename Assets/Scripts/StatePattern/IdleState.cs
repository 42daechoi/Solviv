using UnityEngine;

public class IdleState : IState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("Idle자세");
    }

    public void UpdateState(PlayerController player, Vector3 inputDirection, bool isSprinting)
    {
        if (inputDirection.magnitude > 0.1f)
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
        Debug.Log("Idle자세 벗어남");
    }

    public bool CanInteraction()
    {
        return true;
    }
}