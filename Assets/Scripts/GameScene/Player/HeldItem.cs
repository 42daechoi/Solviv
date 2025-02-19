using GameScene.Item;
using Photon.Pun;
using UnityEngine;

public class HeldItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private Item item;
    [SerializeField] private GameObject itemObject;
    [SerializeField] private int slotIndex;
    [SerializeField] private EquipItem equipItem;
    private float dropOffset = 1f;
    public CheckHeldGun HeldGun;


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
        if (!photonView.IsMine) return;
        if (TryGetComponent(out Inventory inventory))
        {
            if (keyCode == 1)
            {
                equipItem.UnEquip(item, itemObject, true, true);
                InitItemInfo();
            }
            else
            {
                if (item != null)
                {
                    equipItem.UnEquip(item, itemObject, true, true);
                }
                slotIndex = keyCode - 2;
                item = inventory.GetItem(slotIndex);
                itemObject = equipItem.Equip(item);
                
                // '총'인지 판별
                if (itemObject != null)
                {
                    // "CheckHeldGun" 스크립트가 붙어 있다면 '총'으로 간주
                    CheckHeldGun gunCheck = itemObject.GetComponent<CheckHeldGun>();
                    if (gunCheck != null)
                    {
                        HeldGun = gunCheck;
                        Debug.Log("총 장착 완료");
                    }
                    else
                    {
                        HeldGun = null; // 총 아님
                    }
                }
            }
        }
    }

    private void InitItemInfo()
    {
        item = null;
        itemObject = null;
        slotIndex = -10;
        
        HeldGun = null;
    }

    public void ReplaceItem(Vector3 replacePosition, bool needCollider)
    {
        if (!photonView.IsMine) return;
        if (item != null)
        {
            equipItem.UnEquip(item, itemObject, false, needCollider);

            int viewID = itemObject.GetPhotonView().ViewID;
            photonView.RPC("SyncReplaceItem", RpcTarget.All, replacePosition, viewID);
            EventManager_Game.Instance.InvokeRemoveItem(slotIndex);
            InitItemInfo();
        }
    }

    [PunRPC]
    private void SyncReplaceItem(Vector3 replacePosition, int itemViewID)
    {
        PhotonView itemPhotonView = PhotonView.Find(itemViewID);
        if (itemPhotonView == null) return;

        GameObject itemObj = itemPhotonView.gameObject;
        if (itemObj == null) return;

        itemObj.transform.position = replacePosition;
    }

    public void DropItem()
    {
        ReplaceItem(GetDropPosition(), true);
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

    public GameObject GetItemObject()
    {
        return itemObject;
    }


    public void UseItem()
    {
        if (!photonView.IsMine) return;
        if (item == null) return;
        item.UseItem();
    }
}
