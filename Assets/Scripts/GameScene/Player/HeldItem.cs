using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class HeldItem : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private int slotIndex;
    private float dropOffset = 1f;



    private void OnEnable()
    {
        EventManager_Game.Instance.OnHeldItem += SelectItem;
        EventManager_Game.Instance.OnDropItem += DropItem;
    }

    private void OnDisable()
    {
        EventManager_Game.Instance.OnHeldItem -= SelectItem;
        EventManager_Game.Instance.OnDropItem -= DropItem;
    }

    private void SelectItem(int keyCode)
    {
        if (TryGetComponent(out Inventory inventory))
        {
            Item[] itemSlots = inventory.GetItemSlots();

            if (keyCode == 1)
            {
                item = null;
            }
            else
            {
                slotIndex = keyCode - 2;
                item = itemSlots[slotIndex];
                // 아이템 별 애니메이션 추가
            }
        }
    }

    public void DropItem()
    {
        if (item != null && TryGetComponent(out Inventory inventory))
        {
            Item[] itemSlots = inventory.GetItemSlots();
            string dropItemName = itemSlots[slotIndex].itemName;
            Vector3 dropPosition = transform.position + transform.forward * dropOffset;
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, dropOffset))
            {
                // 충돌이 발생하면 드롭 위치를 충돌 지점 바로 앞에 설정
                dropPosition = hit.point - transform.forward * 0.5f;
            }
            ObjectPool.instance.GetObject(dropItemName, dropPosition, transform.rotation);
            EventManager_Game.Instance.InvokeRemoveItem(slotIndex);
            item = null;
        }
    }

    public bool IsHeldItem(string itemName)
    {
        if (item == null)
        {
            return false;
        }
        return item.itemName == itemName;
    }

    public Item GetItem()
    {
        return item;
    }

    public int GetSlotIndex()
    {
        return slotIndex;
    }
}
