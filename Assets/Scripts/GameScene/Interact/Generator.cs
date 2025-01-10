using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Generator : MonoBehaviourPun, IInteractableObject
{
    public void Interact(int playerID)
    {
        GameObject player = PhotonView.Find(playerID).gameObject;
        HeldItem heldItem = player.GetComponent<HeldItem>();

        if (heldItem.IsHeldItem("Battery"))
        {
            return;
        }
        // InstallBattery(); 발전기에 배터리를 장착하는 함수 추가
    }
}
