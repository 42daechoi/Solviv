using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class HeldItem : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private int slotIndex;

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
            Item[] itemSlots = inventory.itemSlots;

            if (keyCode == 1)
            {
                item = null;
                // �Ǽ� ��� �ִϸ��̼� �߰�
            }
            else
            {
                slotIndex = keyCode - 2;
                item = itemSlots[slotIndex];
                // �����ۿ� �´� �ִϸ��̼� �߰�
            }
        }
    }

    private void DropItem()
    {
        if (item != null && TryGetComponent(out Inventory inventory))
        {
            inventory.RemoveItem(slotIndex);
        }
    }

    public bool IsHeldItem(string itemName)
    {
        return item.itemName == itemName;
    }

    public Item GetItem()
    {
        return item;
    }
}
