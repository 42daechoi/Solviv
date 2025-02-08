using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using Object = System.Object;

public class EquipItem : MonoBehaviour
{
    
    private HeldItem _heldItem;
    private GameObject _equippedObject;

    [SerializeField] private Transform _equipPos;

    private void OnEnable()
    {
        EventManager_Game.Instance.OnEquip += EquipHeldItem;
    }

    private void OnDisable()
    {
        EventManager_Game.Instance.OnEquip -= EquipHeldItem;
    }

    private void Start()
    {
        if (TryGetComponent(out _heldItem))
        {
            Debug.Log("가져왔다.", _heldItem);
        }
        else
        {
            Debug.Log("못가져왔다.");
        }
    }

    private void EquipHeldItem()
    {
        if (_heldItem)
        {
            Item item = _heldItem.GetItem();
            if (item)
            {
                Debug.Log($"_heldItem에서 받아온 아이템은 {item}임");
                Equip(item);
            }
            else
            {
                Debug.Log("장착할 대상이 없음");
            }
        }
    }

    private void Equip(Item item)
    {
        // 장착된 무기가 있다면 반환하고 null값 만든 후 실행
        if (_equippedObject)
        {
            ObjectPool.instance.ReturnObject(_equippedObject, _heldItem.GetItem().itemName);
            _equippedObject = null;
        }

        _equippedObject = ObjectPool.instance.GetObject(item.itemName, Vector3.zero, Quaternion.identity);
        _heldItem.SetHeldItemObject(_equippedObject);

        if (_equippedObject)
        {
            _equippedObject.transform.SetParent(_equipPos);
            _equippedObject.transform.localPosition = item.equipPosition;
            _equippedObject.transform.localRotation = Quaternion.Euler(item.equipRotation);
            Debug.Log(_equippedObject);
            
            string animationState = item.itemName == "Battery" ? "Carry" : "Default";
            EventManager_Game.Instance.InvokeAnimationStateChange(animationState);
        }
    }

    public void Unequip()
    {
        if (_equippedObject)
        {
            ObjectPool.instance.ReturnObject(_equippedObject, _heldItem.GetItem().itemName);
            _equippedObject = null;
        }
    }
}
