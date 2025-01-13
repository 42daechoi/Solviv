using UnityEngine;

[CreateAssetMenu(fileName = "Flashlight", menuName = "ScriptableObjects/Flashlight")]
public class Flashlight : Item
{
    public GameObject spotLight;
    public override void UseItem()
    {
        if (spotLight == null)
        {
            Debug.Log("Flashlight.cs : spot light missing.");
        }
        bool isActive = spotLight.activeSelf;
        spotLight.SetActive(!isActive);
    }
}
