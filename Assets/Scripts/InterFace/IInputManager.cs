public interface IInputManager
{
    void HandleCommonInput();    // 공통 입력 처리
    void HandleSpecificInput();  // 각 씬에 맞는 입력 처리
}