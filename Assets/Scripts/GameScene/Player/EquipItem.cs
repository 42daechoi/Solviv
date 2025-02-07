using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Object = System.Object;

public class EquipItem : MonoBehaviour
{
    private HeldItem _heldItem;
    private GameObject _equippedObject;
    
    [SerializeField] private Transform _equipPos;

    [Header("RightHand")] 
    [SerializeField] private TwoBoneIKConstraint _rightHandIK;
    [SerializeField] private Transform _rightHandTarget;

    [Header("LeftHand")] 
    [SerializeField] private TwoBoneIKConstraint _leftHandIK;
    [SerializeField] private Transform _leftHandTarget;

    [SerializeField] private Transform _iKRightHandPos;
    [SerializeField] private Transform _iKLeftHandPos;
    
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
            Debug.Log("가져왔다.",_heldItem);
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
                Debug.Log($"_heldItem에서 받아온아이템은{item}임");
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
            
            if (item.useRightIK)
            {
                _rightHandTarget.position = _equipPos.TransformPoint(item.rightHandPosition);
                _rightHandTarget.rotation = _equipPos.rotation * Quaternion.Euler(item.rightHandRotation);
                _rightHandIK.weight = 1f;
            }
            else
            {
                _rightHandIK.weight = 0f;
            }
            

            // IK 설정: 왼손
            if (item.useLeftIK)
            {
                _leftHandTarget.position = _equipPos.TransformPoint(item.leftHandPosition);
                _leftHandTarget.rotation = _equipPos.rotation * Quaternion.Euler(item.leftHandRotation);
                _leftHandIK.weight = 1f;
            }
            else
            {
                _leftHandIK.weight = 0f;
            }
        }
    }

    public void Unequip()
    {
        if (_equippedObject)
        {
            ObjectPool.instance.ReturnObject(_equippedObject, _heldItem.GetItem().itemName);
            _equippedObject = null;
        }

        _rightHandIK.weight = 0f;
        _leftHandIK.weight = 0f;
    }
}
