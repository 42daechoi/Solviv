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

        // 상호 작용
        if (Input.GetKeyDown(KeyCode.F))
        {
            EventManager_Game.Instance.InvokeInteraction();
        }

        // 아이템 선택
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EventManager_Game.Instance.InvokeHeldItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EventManager_Game.Instance.InvokeHeldItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EventManager_Game.Instance.InvokeHeldItem(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EventManager_Game.Instance.InvokeHeldItem(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EventManager_Game.Instance.InvokeHeldItem(5);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            EventManager_Game.Instance.InvokeUseItem();
        }

        // 아이템 버리기
        if (Input.GetKeyDown(KeyCode.G))
        {
            EventManager_Game.Instance.InvokeDropItem();
        }
    }
}