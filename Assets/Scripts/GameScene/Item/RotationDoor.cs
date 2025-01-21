using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationDoor : Item
{
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;
    [SerializeField] private GameObject doorbutton;

    // 문이 열렸는지 닫혔는지 상태를 표시할 변수
    private bool isDoorOpen = false;

    [SerializeField] private HeldItem heldItem;
    public override void UseItem()
    {
            // 버튼 오브젝트를 맞췄는지 확인하는 조건
            if (CheckButtonHitByRay())
            {
                // 현재 플레이어가 열쇠를 들고 있는지 확인 (이미 구현된 로직을 사용)
                if (HasKeyItem())
                {
                    // 문이 아직 열려있지 않은 상태라면
                    if (!isDoorOpen)
                    {
                        // 문 1: Y축 -90도 회전
                        door1.transform.Rotate(0f, -90f, 0f);

                        // 문 2: Y축 90도 회전
                        door2.transform.Rotate(0f, 90f, 0f);

                        // 문이 열렸다고 표시
                        isDoorOpen = true;
                        Debug.Log("문이 열렸습니다!");
                    }
                }
            }
    }
    
    private bool HasKeyItem()
    {
        //현재 "Key"라는 아이템을 들고 있는지 확인
        return (heldItem != null && heldItem.IsHeldItem("Key"));
    }
    
    // 버튼 오브젝트에 레이가 닿았는지 판별
    private bool CheckButtonHitByRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            // 버튼 오브젝트에 "Button" 태그가 있다고 가정
            if (hit.collider.CompareTag("Button"))
            {
                return true;
            }
        }
        return false;
    }
    
}