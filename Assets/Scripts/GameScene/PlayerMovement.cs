using System.Collections;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    [HideInInspector] public Vector3 dir;
    private float hzInput, vInput;  // hzInput = 좌/우, vInput은 앞/뒤
    public CharacterController _characterController;
    private PhotonView _photonView;

    [SerializeField] private float groundyOffset = 0.1f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;
    private Vector3 charPos;
    private Vector3 velocity;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _photonView = GetComponent<PhotonView>();

        if (_photonView.IsMine)
        {
            EventManager_Game.Instance.OnPlayerMove += MovePlayer;
        }
    }

    private void OnDestroy()
    {
        if (_photonView.IsMine)
        {
            EventManager_Game.Instance.OnPlayerMove -= MovePlayer;
        }
    }


    private void MovePlayer(Vector3 moveDirection)
    {
        if (!_photonView.IsMine)
        {
            Debug.Log("반환됨");
            return;
        }
        dir = transform.forward * moveDirection.z + transform.right * moveDirection.x;

        if (_characterController != null)
        {
            Debug.Log("한캐릭터움직여짐");
            _characterController.Move(dir * Time.deltaTime * moveSpeed);
        }

        ApplyGravity();
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
