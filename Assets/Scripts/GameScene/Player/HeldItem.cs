using UnityEngine;

public class HeldItem : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private GameObject itemObject;
    [SerializeField] private int slotIndex;
    [SerializeField] private EquipItem equipItem;
    private float dropOffset = 1f;



    private void OnEnable()
    {
        EventManager_Game.Instance.OnHeldItem += SelectItem;
        EventManager_Game.Instance.OnDropItem += DropItem;
        EventManager_Game.Instance.OnUseItem += UseItem;
    }

    private void OnDisable()
    {
        EventManager_Game.Instance.OnHeldItem -= SelectItem;
        EventManager_Game.Instance.OnDropItem -= DropItem;
        EventManager_Game.Instance.OnUseItem -= UseItem;
    }

    private void Start()
    {
        if (TryGetComponent(out EquipItem _equipItem))
        {
            equipItem = _equipItem;
        }
        else
        {
            Debug.Log("HeldItem : EquipItem을 가져오지 못함.");
        }
        InitItemInfo();
    }

    private void SelectItem(int keyCode)
    {
        if (TryGetComponent(out Inventory inventory))
        {
            if (keyCode == 1)
            {
                equipItem.UnEquip(item, itemObject, true);
                InitItemInfo();
            }
            else
            {
                if (item != null)
                {
                    equipItem.UnEquip(item, itemObject, true);
                }
                slotIndex = keyCode - 2;
                item = inventory.GetItem(slotIndex);
                itemObject = equipItem.Equip(item);
            }
        }
    }

    private void InitItemInfo()
    {
        item = null;
        itemObject = null;
        slotIndex = -10;
    }

    public void ReplaceItem(Vector3 replacePosition)
    {
        if (item != null)
        {
            equipItem.UnEquip(item, itemObject, false);

            itemObject.transform.position = replacePosition;
            EventManager_Game.Instance.InvokeRemoveItem(slotIndex);
            InitItemInfo();
        }
    }

    public void DropItem()
    {
        ReplaceItem(GetDropPosition());
    }

    private Vector3 GetDropPosition()
    {
        Vector3 dropPosition = transform.position + transform.forward * dropOffset;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, dropOffset))
        {
            // 충돌이 발생하면 드롭 위치를 충돌 지점 바로 앞에 설정
            dropPosition = hit.point - transform.forward * 0.5f;
        }
        return dropPosition;
    }

    public Item GetItem()
    {
        return item;
    }


    public void UseItem()
    {
        if (item == null)
        {
            return;
        }
        item.UseItem();
    }
}
