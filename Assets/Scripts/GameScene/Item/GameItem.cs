using UnityEngine;

[System.Serializable]
public class GameItem
{
    public string itemName;   // 아이템 이름
    public Sprite icon;       // 아이템 아이콘(Sprite)
    public bool isEquipable;  // 장착 가능 여부 (필수 추가)
}