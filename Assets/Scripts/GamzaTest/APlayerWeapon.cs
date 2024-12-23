using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class APlayerWeapon : NetworkBehaviour
{
    public abstract void Fire();
}
