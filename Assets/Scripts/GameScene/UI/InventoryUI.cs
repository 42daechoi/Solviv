using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    public Image[] slotUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateUI(Inventory inventory)
    {
        Item[] itemSlots = inventory.GetItemSlots();
        for (int i = 0; i < slotUI.Length; i++)
        {
            if (i < itemSlots.Length && itemSlots[i] != null)
            {
                slotUI[i].sprite = itemSlots[i].icon;
            }
            else
            {
                slotUI[i].sprite = null;
            }
        }
    }
}
