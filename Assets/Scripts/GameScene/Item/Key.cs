using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Key", menuName = "ScriptableObjects/Key")]
public class Key : Item
{
    public RayModule rayModule;
    public float useDistance = 2f;

    public override void UseItem()
    {
        Debug.Log("2");
        RaycastHit? raycastHit = rayModule.ExecuteRayAction(GetShooterTransform(), 2);
        
        if (raycastHit != null && raycastHit.Value.collider.CompareTag("Button"))
        {
            Debug.Log("3");
            EventManager_Game.Instance.InvokeOpenDoor(this);
        }
    }

    
    private Transform GetShooterTransform()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            return playerObj.transform;
        return null;
    }
}
