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
        // InstallBattery(); �����⿡ ���͸��� �����ϴ� �Լ� �߰�
    }
}
