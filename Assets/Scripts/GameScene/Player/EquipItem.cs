using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class EquipItem : MonoBehaviour
{

    [SerializeField] private Transform _equipTransform;

    public GameObject Equip(Item item)
    {
        if (item == null)
        {
            return null;
        }
        GameObject equipItem = ObjectPool.instance.GetObject(item.itemName, Vector3.zero, Quaternion.identity);
        if (equipItem)
        {
            equipItem.transform.SetParent(_equipTransform);
            equipItem.GetComponent<Collider>().enabled = false;
            equipItem.transform.localPosition = item.equipPosition;
            equipItem.transform.localRotation = Quaternion.Euler(item.equipRotation);
            Debug.Log(equipItem);
            
            string animationState = item.itemName == "Battery" ? "Carry" : "Default";
            EventManager_Game.Instance.InvokeAnimationStateChange(animationState);
        }
        return equipItem;
    }

    public void UnEquip(Item item, GameObject itemObject, bool isReturnPool, bool needCollider)
    {
        if (itemObject)
        {
            if (isReturnPool)
            {
                ObjectPool.instance.ReturnObject(itemObject, item.itemName);
            }
            itemObject.transform.SetParent(null);
            if (needCollider)
            {
                itemObject.GetComponent<Collider>().enabled = true;
            }
        }
    }
}
