using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Farming/Item")]
public abstract class Item : ScriptableObject
{
    [Header("아이템 기본 정보")]
    public string itemName;
    public Sprite icon;

    [Header("Equip Settings")]
    public Vector3 equipPosition;
    public Vector3 equipRotation;
    
    [Header("HandsPosition")]
    public Vector3 rightHandPosition;
    public Vector3 leftHandPosition;

    [Header("HandsRotation")] 
    public Vector3 rightHandRotation;
    public Vector3 leftHandRotation;

    [Header("IK Settings")] 
    public bool useRightIK = true;
    public bool useLeftIK = true;
    public abstract void UseItem();
}