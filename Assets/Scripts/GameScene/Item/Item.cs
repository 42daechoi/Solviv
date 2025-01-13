using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Farming/Item")]
public abstract class Item : ScriptableObject
{
    [Header("아이템 기본 정보")]
    public string itemName;
    public Sprite icon;
    
    public abstract void UseItem();
}