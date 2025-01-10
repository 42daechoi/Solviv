using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Inventory : MonoBehaviourPun
{
    public Item[] itemSlots = new Item[4];
    private float dropOffset = 1f;

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
            string dropItemName = "Item/" + itemSlots[slotIndex].itemName;
            Vector3 dropPosition = transform.position + transform.forward * dropOffset;
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, dropOffset))
            {
                // 충돌이 발생하면 드롭 위치를 충돌 지점 바로 앞에 설정
                dropPosition = hit.point - transform.forward * 0.5f;
            }
            PhotonNetwork.Instantiate(dropItemName, dropPosition, transform.transform.rotation);
            itemSlots[slotIndex] = null;
            InventoryUI.Instance.UpdateUI(this);
            Debug.Log($"{dropItemName}을 버렸습니다.");
        }
        else
        {
            Debug.Log("비어있는 슬롯으로 버리기를 시도할 수 없습니다.");
        }
    }
}
