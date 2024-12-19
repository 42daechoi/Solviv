using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] itemSlots = new Item[4];

    public bool AddItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] == null)
            {
                Debug.Log($"{item.itemName}을 획득하였습니다.");
                itemSlots[i] = item;
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
            Debug.Log($"{itemSlots[slotIndex].itemName}을 버렸습니다.");
        }
        else
        {
            Debug.Log("비어있는 슬롯으로 버리기를 시도할 수 없습니다.");
        }
    }
}
