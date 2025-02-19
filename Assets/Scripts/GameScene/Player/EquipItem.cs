using UnityEngine;
using Photon.Pun;

public class EquipItem : MonoBehaviourPunCallbacks
{

    [SerializeField] private Transform _equipTransform;

    public GameObject Equip(Item item)
    {
        if (!photonView.IsMine || item == null)
        {
            return null;
        }
        GameObject equipItem = ObjectPool.instance.GetObject(item.itemName, Vector3.zero, Quaternion.identity);
        if (equipItem == null)
        {
            Debug.Log("EquipItem : 오브젝트 풀에서 장착할 아이템을 받아오지 못했습니다.");
            return null;
        }
        int viewID = equipItem.GetPhotonView().ViewID;
        if (equipItem)
        {
            photonView.RPC("SyncEquipItem", RpcTarget.All, viewID, item.equipPosition, item.equipRotation, photonView.ViewID);
            string animationState = item.itemName == "Battery" ? "Carry" : "Default";
            EventManager_Game.Instance.InvokeAnimationStateChange(animationState);
        }
        return equipItem;
    }

    private Transform GetEquipTransform()
    {
        return _equipTransform;
    }

    [PunRPC]
    private void SyncEquipItem(int viewID, Vector3 equipPosition, Vector3 equipRotation, int playerViewID)
    {
        GameObject equipItem = PhotonView.Find(viewID).gameObject;

        PhotonView playerPhotonView = PhotonView.Find(playerViewID);
        if (playerPhotonView == null) return;

        EquipItem localEquipItem = playerPhotonView.GetComponent<EquipItem>();
        if (localEquipItem == null) return;

        equipItem.transform.SetParent(localEquipItem._equipTransform);
        equipItem.GetComponent<Collider>().enabled = false;
        equipItem.transform.localPosition = equipPosition;
        equipItem.transform.localRotation = Quaternion.Euler(equipRotation);
    }

    public void UnEquip(Item item, GameObject itemObject, bool isReturnPool, bool needCollider)
    {
        if (!photonView.IsMine) return;
        if (itemObject)
        {
            if (isReturnPool)
            {
                ObjectPool.instance.ReturnObject(itemObject, item.itemName);
            }
            int viewID = itemObject.GetPhotonView().ViewID;
            photonView.RPC("SyncUnequip", RpcTarget.All, viewID, needCollider);
        }
    }

    [PunRPC]
    private void SyncUnequip(int viewID, bool needCollider)
    {
        GameObject unequipItem = PhotonView.Find(viewID).gameObject;
        unequipItem.transform.SetParent(null);
        if (needCollider)
        {
            unequipItem.GetComponent<Collider>().enabled = true;
        }
    }
}
