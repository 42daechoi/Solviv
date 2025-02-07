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
        batteryPositionOffset = Vector3.zero;
        // 추후 수정 필요, 배터리 장착시 오프셋 값 증가 필요, 배터리 해제 시 오프셋 값 감소 필요
    }

    public bool TryInstallBattery(HeldItem heldItem)
    {
        if (IsAllBatteryInstalled())
        {
            Debug.Log("Generator : 배터리가 이미 가득 찼습니다.");
            return false;
        }
        Debug.Log("3");
        InstallBattery(heldItem);
        if (IsAllBatteryInstalled())
        {
            ExecuteGenerator();
        }
        return true;
    }

    private void InstallBattery(HeldItem heldItem)
    {
        heldItem.ReplaceItem(gameObject.transform.position + batteryPositionOffset);
        currentBatteryCount++;
        Debug.Log($"Generator : 배터리 장착 성공. 현재 장착된 배터리 갯수 : {currentBatteryCount}");
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
