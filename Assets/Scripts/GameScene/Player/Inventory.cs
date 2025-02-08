using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Inventory : MonoBehaviourPun
{
    [SerializeField]private Item[] itemSlots = new Item[4];
    [SerializeField] private Item fistItem;

    private void Start()
    {
        if (fistItem != null)
        {
            itemSlots[0] = fistItem;
        }
        else
        {
            Debug.LogError("주먹 아이템이 설정되지 않았습니다.");
        }
        
        InventoryUI.Instance.UpdateUI(this);
    }


    private void OnEnable()
    {
        EventManager_Game.Instance.OnRemoveItem += RemoveItem;
    }

    private void OnDisable()
    {
        EventManager_Game.Instance.OnRemoveItem -= RemoveItem;
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] == null)
            {
                Debug.Log($"{item.itemName}을 획득하였습니다.");
                itemSlots[i] = item;
                InventoryUI.Instance.UpdateUI(this);
                return true;
            }
        }
        Debug.Log("인벤토리가 가득 찼습니다.");
        return false;
    }

    public void RemoveItem(int slotIndex)
    {
        if (itemSlots[slotIndex] != null)
        {
            itemSlots[slotIndex] = null;
            InventoryUI.Instance.UpdateUI(this);
        }
        else
        {
            Debug.Log("비어있는 슬롯으로 버리기를 시도할 수 없습니다.");
        }
    }

    public Item GetItem(int slotIndex)
    {
        return itemSlots[slotIndex];
    }

    public Item[] GetItemSlots()
    {
        return itemSlots;
    }
}
