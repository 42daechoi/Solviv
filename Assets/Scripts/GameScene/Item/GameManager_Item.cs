using UnityEngine;

public class GameManager_Item : MonoBehaviour
{
    public InventoryManager inventoryManager;

    void Start()
    {
        // TestSprite 폴더의 모든 Sprite를 슬롯에 로드
        inventoryManager.LoadSpritesToSlots();
    }
}