using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InventoryManager inventoryManager;

    void Update()
    {
        // 숫자 키 입력에 따라 슬롯 선택
        if (Input.GetKeyDown(KeyCode.Alpha1)) inventoryManager.SelectSlot(0); // 슬롯 1
        if (Input.GetKeyDown(KeyCode.Alpha2)) inventoryManager.SelectSlot(1); // 슬롯 2
        if (Input.GetKeyDown(KeyCode.Alpha3)) inventoryManager.SelectSlot(2); // 슬롯 3
        if (Input.GetKeyDown(KeyCode.Alpha4)) inventoryManager.SelectSlot(3); // 슬롯 4
        if (Input.GetKeyDown(KeyCode.Alpha5)) inventoryManager.SelectSlot(4); // 슬롯 5
    }
}