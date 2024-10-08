using Photon.Pun;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView photonView;

    [SerializeField]
    private float speed = 4.0f;

    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    private Vector3 _moveDirection;
    private Quaternion _targetRotation;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
            virtualCamera.gameObject.SetActive(true);
            
            EventManager.Instance.PlayerMove += OnPlayerMove;
        }
        else
        {
            virtualCamera.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            EventManager.Instance.PlayerMove -= OnPlayerMove;
        }
    }

    private void OnPlayerMove(Vector3 moveDirection)
    {
        _moveDirection = moveDirection;
        
        if (moveDirection != Vector3.zero)
        {
            _targetRotation = Quaternion.LookRotation(moveDirection);
            MoveAndRotatePlayer();
        }
    }
    
    private void MoveAndRotatePlayer()
    {
        Vector3 newPosition = transform.position + _moveDirection * speed * Time.deltaTime;
        Quaternion newRotation = Quaternion.Slerp(transform.rotation, _targetRotation, 0.2f);

        // 자신의 움직임이 발생했을 때만 RPC 호출
        if (newPosition != transform.position || newRotation != transform.rotation)
        {
            // RPC 호출
            photonView.RPC("Move", RpcTarget.All, newPosition, newRotation);
        }
    }

    [PunRPC]
    public void Move(Vector3 newPosition, Quaternion newRotation)
    {
        transform.position = newPosition;
        transform.rotation = newRotation;
    }
}
