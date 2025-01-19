using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Generator : MonoBehaviourPun, IInteractableObject
{
    private int maxBatteryCount = 3;
    [SerializeField] private int currentBatteryCount;

    void Start()
    {
        currentBatteryCount = 0;
    }

    public void Interact(int playerID)
    {
        GameObject player = PhotonView.Find(playerID).gameObject;
        HeldItem heldItem = player.GetComponent<HeldItem>();
        Inventory inventory = player.GetComponent<Inventory>();

        if (!heldItem.IsHeldItem("Battery") || currentBatteryCount >= maxBatteryCount)
        {
            return;
        }
        InstallBattery(inventory, heldItem.GetSlotIndex());
        ExecuteGenerator();
    }

    private void InstallBattery(Inventory inventory, int slotIdx)
    {
        // 발전기에 배터리 장착 위치 설정 추가 필요
        EventManager_Game.Instance.InvokeRemoveItem(slotIdx);
        currentBatteryCount++;
        Debug.Log($"배터리 장착 성공. 현재 장착된 배터리 갯수 : {currentBatteryCount}");
    }

    private void ExecuteGenerator()
    {
        if (currentBatteryCount >= maxBatteryCount)
        {
            // 발전기 가동 애니메이션 또는 발전기 Light On
            // GameManager의 실행된 발전기 수 증가
            Debug.Log("발전기 가동 완료.");
        }
    }
}
