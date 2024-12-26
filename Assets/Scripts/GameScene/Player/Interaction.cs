using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Interaction : MonoBehaviourPun
{
    public float interactionRange = 3.0f;

    private void OnEnable()
    {
        EventManager_Game.Instance.OnInteraction += TryInteraction;
    }

    private void OnDisable()
    {
        EventManager_Game.Instance.OnInteraction -= TryInteraction;
    }

    private void TryInteraction(bool interaction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
        {
            FarmingObject farmingObject = hit.collider.GetComponent<FarmingObject>();
            if (farmingObject != null)
            {
                farmingObject.Interact(photonView.ViewID);
            }
        }
    }
}
