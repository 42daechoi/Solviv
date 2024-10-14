using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager_Lobby : MonoBehaviour
{
    public Vector3 redSpawnAreaCenter;      // 레드팀 스폰 영역의 중심
    public Vector3 blueSpawnAreaCenter;     // 블루팀 스폰 영역의 중심
    public float spawnAreaRadius = 4f;      // 스폰 범위의 반지름
    public float checkRadius = 0.5f;        // 충돌 감지를 위한 반지름
    public int maxAttempts = 10;            // 스폰 위치를 찾기 위한 최대 시도 횟수

    void Start()
    {
        redSpawnAreaCenter = new Vector3(-18, 150, 8);
        blueSpawnAreaCenter = new Vector3(-18, 10, 78);
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        bool positionFound = false;
        Vector3 spawnPosition = Vector3.zero;

        for (int i = 0; i < maxAttempts; i++)
        {
            // 스폰 영역 안에서 랜덤 위치 생성
            spawnPosition = GetRandomPositionWithinArea();

            // 해당 위치 주변에 충돌체가 있는지 검사
            if (!Physics.CheckSphere(spawnPosition, checkRadius))
            {
                positionFound = true;
                break;  // 충돌이 없으면 스폰 위치로 확정하고 반복 종료
            }
        }

        if (positionFound)
        {
            // 플레이어를 스폰
            GameObject redPlayer = PhotonNetwork.Instantiate("PicoChan", spawnPosition, Quaternion.identity);
            PhotonView pv = redPlayer.GetComponent<PhotonView>();

            if (pv != null)
            {
                pv.TransferOwnership(PhotonNetwork.LocalPlayer); // 현재 플레이어에게 소유권 부여
            }
            Debug.Log("aa");
        }
        else
        {
            Debug.LogWarning("충돌 없이 스폰할 수 있는 위치를 찾지 못했습니다.");
        }
    }

    Vector3 GetRandomPositionWithinArea()
    {
        // 랜덤한 위치를 스폰 범위 내에서 생성
        Vector3 randomPosition = redSpawnAreaCenter + Random.insideUnitSphere * spawnAreaRadius;
        randomPosition.y = redSpawnAreaCenter.y;  // y 값 고정

        return randomPosition;
    }
}
