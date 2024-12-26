using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon;  // 슬롯에 표시될 아이콘 이미지
    private GameItem item;  // 슬롯에 들어갈 아이템 데이터

    // 슬롯에 아이템 추가
    public void SetItem(GameItem newItem)
    {
        item = newItem;             // 아이템 데이터를 슬롯에 저장
        icon.sprite = item.icon;    // 슬롯 아이콘에 아이템 Sprite 표시
        icon.enabled = true;        // 아이콘 활성화
    }

    // 슬롯을 비움
    public void ClearSlot()
    {
        item = null;                // 슬롯의 아이템 데이터 제거
        icon.sprite = null;         // 슬롯 아이콘 제거
        icon.enabled = false;       // 아이콘 비활성화
    }

    // 슬롯에 들어있는 아이템 반환
    public GameItem GetItem()
    {
        return item;
    }

    // 슬롯 클릭 시 동작
    public void OnSlotClick()
    {
        if (item != null)
        {
            Debug.Log($"아이템 {item.itemName} 사용!");
            // 아이템 장착 또는 사용 로직 추가
        }
    }
}