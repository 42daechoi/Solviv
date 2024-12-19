using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera firstPersonCamera; // 1인칭 Cinemachine Virtual Camera
    public Transform playerTransform; // 캐릭터 Transform (Follow/LookAt 대상)

    void Start()
    {
        // 1인칭 카메라 초기화
        if (firstPersonCamera != null && playerTransform != null)
        {
            firstPersonCamera.Follow = playerTransform;
            firstPersonCamera.LookAt = playerTransform;
            firstPersonCamera.Priority = 10; // Priority 값을 높여 활성화
        }
        else
        {
            Debug.LogError("First Person Camera or Player Transform is not assigned.");
        }
    }
}