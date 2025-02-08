using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettings", menuName = "Settings/MovementSettings", order = 1)]
public class MovementSettings : ScriptableObject
{
    public float walkSpeed = 800f;
    public float sprintSpeed = 1000f;
    public float maxVelocityChange = 600f;
    public float jumpForce = 400f;
}
