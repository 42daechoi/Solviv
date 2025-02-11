using Photon.Realtime;
using UnityEngine;

public class InputManager_Game : MonoBehaviour
{
    private PlayerController _playerController;

    void Start()
    {
        _playerController = PlayerController.LocalPlayerInstance;

        if (_playerController == null)
        {
            Debug.LogError("플레이어컨트롤러 싱글톤 null떳다잉");
            return;
        }
}
    
    void Update()
    {
        IState currentState = _playerController.GetCurrentState();
        
        if (currentState is UseComputerState)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                EventManager_Game.Instance.InvokeInteraction();
            }
            return;
        }
        
        // 이동 입력
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Mathf.Abs(horizontal) > 0.01f || Mathf.Abs(vertical) > 0.01f)
        {
            EventManager_Game.Instance.InvokePlayerMove(horizontal, vertical);
        }
        
        // 스프린트
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        EventManager_Game.Instance.InvokeSprint(isSprinting);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventManager_Game.Instance.InvokePlayerJump();
        }
        
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