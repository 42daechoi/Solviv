using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public interface IInteractableObject
{
    void Interact(int playerId);
}
