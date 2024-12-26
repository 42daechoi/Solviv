using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Slot[] slots;            // 슬롯 배열
    public List<Item> items;        // 아이템 데이터 리스트
    private int selectedSlotIndex = -1; // 현재 선택된 슬롯 (기본값: 선택되지 않음)

    // 아이템 추가 (빈 슬롯에 추가)
    public void AddItem(GameItem newItem)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetItem() == null) // 빈 슬롯 확인
            {
                slots[i].SetItem(newItem); // 슬롯에 아이템 추가
                return;
            }
        }
        Debug.Log("슬롯이 가득 찼습니다!");
    }

    // TestSprite 폴더에서 모든 Sprite 로드하여 슬롯에 배치
    public void LoadSpritesToSlots()
    {
        Sprite[] allSprites = Resources.LoadAll<Sprite>("TestSprite"); // TestSprite 폴더에서 모든 Sprite 불러오기
        foreach (Sprite sprite in allSprites)
        {
            // Sprite 데이터를 기반으로 새로운 Item 생성
            GameItem newItem = new GameItem
            {
                itemName = sprite.name, // Sprite 이름을 아이템 이름으로 설정
                icon = sprite,          // Sprite를 아이콘으로 설정
                isEquipable = true      // 장착 가능 여부 설정
            };

            AddItem(newItem); // 슬롯에 아이템 추가
        }
    }

    // 슬롯 선택
    public void SelectSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length) // 슬롯 범위 확인
        {
            selectedSlotIndex = slotIndex; // 선택된 슬롯 인덱스 저장
            GameItem selectedItem = slots[slotIndex].GetItem();

            if (selectedItem != null)
            {
                Debug.Log($"슬롯 {slotIndex + 1} 선택됨: {selectedItem.itemName}");
                // 선택된 아이템 사용 또는 장착 로직 추가
            }
            else
            {
                Debug.Log($"슬롯 {slotIndex + 1}은 비어 있습니다.");
            }
        }
        else
        {
            Debug.LogWarning("유효하지 않은 슬롯 인덱스입니다.");
        }
    }
}