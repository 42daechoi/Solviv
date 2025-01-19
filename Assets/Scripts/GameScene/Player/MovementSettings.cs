using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettings", menuName = "Settings/MovementSettings", order = 1)]
public class MovementSettings : ScriptableObject
{
    public float walkSpeed = 4f;
    public float sprintSpeed = 8f;
    public float maxVelocityChange = 10f;
    public float smoothSpeed = 0.2f;
}