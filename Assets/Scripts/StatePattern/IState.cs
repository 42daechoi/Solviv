public interface IState
{
    void EnterState(PlayerController player);
    void UpdateState(PlayerController player);
    void FixedUpdateState(PlayerController player);
    void ExitState(PlayerController player);

    bool CanInteraction(); // Idle 인터렉션 테스트용 필요없으면 지울예정
}
