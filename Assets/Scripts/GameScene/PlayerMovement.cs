using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    [HideInInspector] public Vector3 dir;
    private float hzInput, vInput;  // hzInput = ��/��, vInput�� ��/��
    public CharacterController _characterController;

    [SerializeField] private float groundyOffset = 0.1f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;
    private Vector3 charPos;
    private Vector3 velocity;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        
        // EventManager_Game���� OnPlayerMove �̺�Ʈ ����
        BaseEventManager.Instance.OnPlayerMove += MovePlayer;
        BaseEventManager.Instance.OnJump += JumpPlayer;
    }
    
    private void OnDestroy()
    {
        // ������Ʈ �ı� �� �̺�Ʈ ���� ����
        BaseEventManager.Instance.OnPlayerMove -= MovePlayer;
        BaseEventManager.Instance.OnJump -= JumpPlayer;
    }
    

    private void MovePlayer(Vector3 moveDirection)
    {
        dir = transform.forward * moveDirection.z + transform.right * moveDirection.x;

        if (_characterController != null)
        {
            _characterController.Move(dir * Time.deltaTime * moveSpeed);
        }

        ApplyGravity();
    }

    private void JumpPlayer()
    {
        Debug.Log("�����ߴ�");
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
