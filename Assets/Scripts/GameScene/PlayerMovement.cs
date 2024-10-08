using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Cinemachine;
public class PlayerMovement : MonoBehaviour
{
    private PhotonView photonView;

    [SerializeField] 
    private float _speed = 2.0f;
    
    [SerializeField] 
    private CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        
        if (photonView.IsMine)
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
            virtualCamera.gameObject.SetActive(true);
        }
        else
        {
            virtualCamera.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            Vector3 newPosition = transform.position;
            Quaternion newRotation = transform.rotation;

            if (Input.GetKey(KeyCode.W))
            {
                newRotation = Quaternion.Slerp(newRotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
                newPosition += Vector3.forward * Time.deltaTime * _speed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                newRotation = Quaternion.Slerp(newRotation, Quaternion.LookRotation(Vector3.back), 0.2f);
                newPosition += Vector3.back * Time.deltaTime * _speed;
            }

            if (Input.GetKey(KeyCode.A))
            {
                newRotation = Quaternion.Slerp(newRotation, Quaternion.LookRotation(Vector3.left), 0.2f);
                newPosition += Vector3.left * Time.deltaTime * _speed;
            }

            if (Input.GetKey(KeyCode.D))
            {
                newRotation = Quaternion.Slerp(newRotation, Quaternion.LookRotation(Vector3.right), 0.2f);
                newPosition += Vector3.right * Time.deltaTime * _speed;
            }

            // 자신의 움직임이 발생했을 때만 RPC 호출
            if (newPosition != transform.position || newRotation != transform.rotation)
            {
                // RPC 호출
                photonView.RPC("Move", RpcTarget.All, newPosition, newRotation);
            }
        }
    }


    [PunRPC]
    public void Move(Vector3 newPosition, Quaternion newRotation)
    {
        transform.position = newPosition;
        transform.rotation = newRotation;
    }

}
