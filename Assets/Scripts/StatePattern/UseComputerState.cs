using UnityEngine;

public class UseComputerState : IState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("UseComputerState에 진입했습니다.");
    }

    public void UpdateState(PlayerController player, Vector3 inputDirection, bool isSprinting)
    {
        
    }

    public void UpdateState(Vector3 inputDirection, bool isSprinting)
    {
        
    }

    public void FixedUpdateState(PlayerController player)
    {
        
    }

    public void ExitState(PlayerController player)
    {
        Debug.Log("UseComputerState에서 종료");

    }

    public bool CanInteraction()
    {
        return true;
    }
}
