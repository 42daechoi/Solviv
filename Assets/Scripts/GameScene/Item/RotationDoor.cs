using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationDoor : MonoBehaviour
{
    [SerializeField] private Transform door1;
    [SerializeField] private Transform door2;
    //[SerializeField] private GameObject doorbutton;

    // 문이 열렸는지 닫혔는지 상태를 표시할 변수
    private bool isDoorOpen = false;
    
    [SerializeField] private float door1OpenAngle = 90f;
    [SerializeField] private float door2OpenAngle = -90f;
    
    [SerializeField] private Vector3 rotationAxis = Vector3.up;

    private bool isFocused = false;
    
    private void OnEnable()
    {
        if (EventManager_Game.Instance != null)
        {
            EventManager_Game.Instance.OnUseItemWithItem += HandleUseItemWithItem;
        }
    }

    private void OnDisable()
    {
        if (EventManager_Game.Instance != null)
        {
            EventManager_Game.Instance.OnUseItemWithItem -= HandleUseItemWithItem;
        }
    }
    
    public void SetFocus(bool focus)
    {
        isFocused = focus;
    }
    
    private void HandleUseItemWithItem(Item usedItem)
    {
        // (1) 이번에 아이템이 사용되었지만, 이 문이 포커스 상태가 아니라면 무시
        if (!isFocused) return;

        // (2) 사용된 아이템이 "Key"인지 확인 (추상클래스 Item을 상속받는 Key)
        Key keyItem = usedItem as Key;
        if (keyItem == null)
        {
            // 키가 아님
            return;
        }

        // (3) 여기까지 왔다면 => 문 열기
        OpenBothDoors();

        // (4) 한 번 사용 후 포커스 해제 (재사용 방지)
        isFocused = false;
    }

    private void OpenBothDoors()
    {
        // door1을 rotationAxis 기준 door1OpenAngle만큼 회전
        if (door1 != null)
        {
            door1.Rotate(rotationAxis, door1OpenAngle);
        }

        // door2를 rotationAxis 기준 door2OpenAngle만큼 회전
        if (door2 != null)
        {
            door2.Rotate(rotationAxis, door2OpenAngle);
        }
    }
    
}