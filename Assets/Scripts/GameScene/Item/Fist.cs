using UnityEngine;

[CreateAssetMenu(fileName = "Fist", menuName = "ScriptableObjects/Fist")]
public class Fist : Item
{
    public override void UseItem()
    {
        Debug.Log("공포의 쓴맛!");
    }
}