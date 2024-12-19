using UnityEngine;

public class InputManager_Game : MonoBehaviour
{
    void Update()
    {
        // 이동 입력 처리
        float hzInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(hzInput, 0, vInput);

        // 이동 이벤트 호출
        EventManager_Game.Instance.InvokePlayerMove(moveDirection);

        // 스프린트 입력 처리
        bool sprinting = Input.GetKey(KeyCode.LeftShift); // KeyCode로 처리
        EventManager_Game.Instance.InvokePlayerSprint(sprinting);
    }
}