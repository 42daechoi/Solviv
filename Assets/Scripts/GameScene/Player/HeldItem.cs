using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItem : MonoBehaviour
{
    public Item item;

    private void OnEnable()
    {
        EventManager_Game.Instance.OnHeldItem += SelectItem;
    }

    private void OnDisable()
    {
        EventManager_Game.Instance.OnHeldItem -= SelectItem;
    }

    public void SelectItem(int keyCode)
    {
        Inventory inventory = GetComponent<Inventory>();
        Item[] itemSlots = inventory.itemSlots;

        if (keyCode == 1)
        {
            item = null;
            // 맨손 드는 애니메이션 추가
        }
        else
        {
            item = itemSlots[keyCode - 2];
            // 아이템에 맞는 애니메이션 추가
        }
    }

    public string GetHeldItemName()
    {
        return item.itemName;
    }
}
