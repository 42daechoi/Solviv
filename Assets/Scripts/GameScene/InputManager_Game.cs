using UnityEngine;

public class InputManager_Game : MonoBehaviour
{
    void Update()
    {
        
        // 이동 입력
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveDirection.magnitude > 0.01f) // 이동 입력이 유효할 때만 이벤트 발행
        {
            EventManager_Game.Instance.InvokePlayerMove(moveDirection);
        }
        
        // 스프린트
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        EventManager_Game.Instance.InvokeSprint(isSprinting);
        
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