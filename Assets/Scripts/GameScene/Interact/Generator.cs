using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Generator : MonoBehaviourPun, IInteractableObject
{
    private int maxBatteryCount = 3;
    [SerializeField] private int installedBatteryCount;
    [SerializeField] private GameObject[] installedBattery;
    private Vector3 batteryPositionOffset;

    void Start()
    {
        installedBatteryCount = 0;
        installedBattery = new GameObject[3];
        batteryPositionOffset = new Vector3(-0.4f, 0.5f, 0.3f);
    }

    public void Interact(int playerId)
    {
        if (installedBatteryCount != 0)
        {
            UninstallBattery(playerId);
        }
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

        installedBattery[installedBatteryCount] = heldItem.GetItemObject();
        heldItem.ReplaceItem(worldPosition, false);
        Debug.Log($"Generator : 배터리 장착 성공. 장창된 포지션 : {worldPosition}");
        IncreaseBatteryCount();
        Debug.Log($"Generator : 배터리 장착 성공. 현재 장착된 배터리 갯수 : {installedBatteryCount}");
    }

    private void UninstallBattery(int playerId)
    {
        DecreaseBatteryCount();
        GameObject uninstallBatteryObject = installedBattery[installedBatteryCount];
        FarmingObject farmingObject = uninstallBatteryObject.GetComponent<FarmingObject>();
        farmingObject.Interact(playerId);
        installedBattery[installedBatteryCount] = null;
        Debug.Log($"Generator : 배터리 회수 성공. 현재 장착된 배터리 갯수 : {installedBatteryCount}");

    }

    private void IncreaseBatteryCount()
    {
        installedBatteryCount++;
        batteryPositionOffset += new Vector3(0.4f, 0, 0);
    }

    private void DecreaseBatteryCount()
    {
        installedBatteryCount--;
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
        return installedBatteryCount >= maxBatteryCount;
    }
}
