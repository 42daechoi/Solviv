using UnityEngine;

public class UseComputerState : IState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("UseComputerState에 진입했습니다.");
        player.StartMoveToComputer();
    }

    public void UpdateState(PlayerController player, Vector3 inputDirection, float offset)
    {
        
    }
    

    public void FixedUpdateState(PlayerController player, Vector3 inputDirection, float offset)
    {
        
    }

    public void ExitState(PlayerController player)
    {
        Debug.Log("UseComputerState에서 종료");

    }

    public bool CanInteraction()
    {
        return false;
    }
}
