using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Generator : MonoBehaviourPun
{
    private int maxBatteryCount = 3;
    [SerializeField] private int currentBatteryCount;
    private Vector3 batteryPositionOffset;

    void Start()
    {
        currentBatteryCount = 0;
        batteryPositionOffset = new Vector3(-0.4f, 0.5f, 0.3f);
        // 추후 수정 필요, 배터리 장착시 오프셋 값 증가 필요, 배터리 해제 시 오프셋 값 감소 필요
    }

    public void TryInstallBattery(HeldItem heldItem)
    {
        if (IsAllBatteryInstalled())
        {
            Debug.Log("Generator : 배터리가 이미 가득 찼습니다.");
        }
        InstallBattery(heldItem);
        if (IsAllBatteryInstalled())
        {
            ExecuteGenerator();
        }
    }

    private void InstallBattery(HeldItem heldItem)
    {
        Vector3 worldPosition = transform.position + transform.right * batteryPositionOffset.x
                                          + transform.up * batteryPositionOffset.y
                                          + transform.forward * batteryPositionOffset.z;

        heldItem.ReplaceItem(worldPosition);
        IncreaseBattery();
        Debug.Log($"Generator : 배터리 장착 성공. 현재 장착된 배터리 갯수 : {currentBatteryCount}");
    }

    private void IncreaseBattery()
    {
        currentBatteryCount++;
        batteryPositionOffset += new Vector3(0.4f, 0, 0);
    }

    private void DecreaseBattery()
    {
        currentBatteryCount--;
        batteryPositionOffset -= new Vector3(0.4f, 0, 0);
    }

    private void ExecuteGenerator()
    {
        if (IsAllBatteryInstalled())
        {
            // 발전기 가동 애니메이션 또는 발전기 Light On
            GameManager.Instance.AddActiveGenerator();
            Debug.Log("Generator : 발전기 가동 완료.");
        }
    }

    private bool IsAllBatteryInstalled()
    {
        return currentBatteryCount >= maxBatteryCount;
    }
}
