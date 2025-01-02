using Photon.Pun;
using UnityEngine;

public class FarmingObject : MonoBehaviourPun, IInteractableObject
{
    public Item item;

    [PunRPC]
    public void FarmingItem(int playerID)
    {
        GameObject player = PhotonView.Find(playerID).gameObject;
        Inventory playerInventory = player.GetComponent<Inventory>();

        if (playerInventory.AddItem(item))
        {
            PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            Debug.Log("FarmingObject.cs : 아이템 파밍 실패");
        }
    }

    public void Interact(int playerID)
    {
        photonView.RPC("FarmingItem", RpcTarget.All, playerID);
    }
}
