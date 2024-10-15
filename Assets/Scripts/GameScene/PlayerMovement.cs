using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    [HideInInspector] public Vector3 dir;
    private float hzInput, vInput;  // hzInput = ÁÂ/¿ì, vInputÀº ¾Õ/µÚ
    public CharacterController _characterController;

    [SerializeField] private float groundyOffset = 0.1f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;
    private Vector3 charPos;
    private Vector3 velocity;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GetDirectionAndMove();
        ApplyGravity();
    }
    
    private void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        
        dir = transform.forward * vInput + transform.right * hzInput;
        
        if (_characterController != null)
        {
            _characterController.Move(dir * Time.deltaTime * moveSpeed);
        }
    }
    private bool IsGrounded()
    {
        charPos = new Vector3(transform.position.x, transform.position.y - groundyOffset, transform.position.z);

        return Physics.CheckSphere(charPos, _characterController.radius - 0.05f, groundMask);
    }
    
    private void ApplyGravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        if (_characterController != null)
        {
            _characterController.Move(velocity * Time.deltaTime);
        }
    }
    
    private void OnDrawGizmos()
    {
        if (_characterController != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(charPos, _characterController.radius - 0.05f);
        }
    }
}
