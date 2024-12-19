using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Image[] slotUI;

    void UpdateUI()
    {
        for (int i = 0; i < slotUI.Length; i++)
        {
            if (i < inventory.itemSlots.Length && inventory.itemSlots[i] != null)
            {
                slotUI[i].sprite = inventory.itemSlots[i].icon;
            }
            else
            {
                slotUI[i].sprite = null;
            }
        }
    }
}
