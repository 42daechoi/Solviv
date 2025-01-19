using UnityEngine;

public interface IState
{
    void EnterState(PlayerController player);
    void UpdateState(PlayerController player, Vector3 inputDirection, bool isSprinting);
    void FixedUpdateState(PlayerController player);
    void ExitState(PlayerController player);
    bool CanInteraction();
}
