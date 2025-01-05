using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;
using Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    public Cinemachine.AxisState xAxis, yAxis;
    private PhotonView _photonView;

    [SerializeField] Transform camFollowPos;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();

        if (_photonView.IsMine)
        {
            if (virtualCamera != null && camFollowPos != null)
            {
                virtualCamera.Follow = camFollowPos;
                virtualCamera.LookAt = camFollowPos;
                virtualCamera.gameObject.SetActive(true);

                // Axis 초기화
                xAxis.Reset();
                yAxis.Reset();
            }
        }
        else
        {
            if (virtualCamera != null)
            {
                virtualCamera.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (_photonView.IsMine)
        {
            xAxis.Update(Time.deltaTime);
            yAxis.Update(Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        if (_photonView.IsMine)
        {
            if (virtualCamera.Follow == null || virtualCamera.LookAt == null)
            {
                virtualCamera.Follow = camFollowPos;
                virtualCamera.LookAt = camFollowPos;
            }

            Vector3 cameraRotation = virtualCamera.transform.localEulerAngles;
            cameraRotation.x = yAxis.Value;
            virtualCamera.transform.localEulerAngles = cameraRotation;
            transform.eulerAngles = new Vector3(0f, xAxis.Value, 0f);
        }
    }
}