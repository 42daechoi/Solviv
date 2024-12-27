using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Farming/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
}