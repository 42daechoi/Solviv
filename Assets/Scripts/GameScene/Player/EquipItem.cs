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
        EventManager_Game.Instance.OnUnequipItem += Unequip;
    }

    private void OnDisable()
    {
        EventManager_Game.Instance.OnEquip -= EquipHeldItem;
        EventManager_Game.Instance.OnUnequipItem -= Unequip;
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
        }
    }

    private void Equip(Item item)
    {
        if (_equippedObject)
        {
            string equippedItemName = _heldItem?.GetItem()?.itemName;
            if (!string.IsNullOrEmpty(equippedItemName))
            {
                Debug.Log("이즈널올엠프티 들어갔음");
                ObjectPool.instance.ReturnObject(_equippedObject, equippedItemName);
            }
            _equippedObject = null;
        }

        _equippedObject = ObjectPool.instance.GetObject(item.itemName, Vector3.zero, Quaternion.identity);
        
        if (_equippedObject == null)
        {
            Debug.LogError($"ObjectPool에서 {item.itemName}을(를) 가져오지 못했습니다.");
            return;
        }
        
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
            // heldItem이 null이거나 아이템 이름이 null인 경우 반환을 생략
            string itemName = _heldItem?.GetItem()?.itemName;

            if (!string.IsNullOrEmpty(itemName))
            {
                ObjectPool.instance.ReturnObject(_equippedObject, itemName);
            }

            _equippedObject = null;
        }

        // 애니메이션 상태를 기본으로 전환
        EventManager_Game.Instance.InvokeAnimationStateChange("Default");
    }
    
}
