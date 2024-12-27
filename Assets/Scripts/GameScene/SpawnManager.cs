using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    private bool[] isSpawned;

    void Start()
    {
        isSpawned = new bool[spawnPoints.Length];
        SpawnPlayer();
        
    }

    void SpawnPlayer()
    {
        int playerIdx = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        int spawnIdx = GetAvailableSpawnIndex(playerIdx);

        if (spawnIdx < 0) return;
        Vector3 spawnPosition = spawnPoints[spawnIdx].position;
        Quaternion spawnRotation = spawnPoints[spawnIdx].rotation;
        GameObject player = PhotonNetwork.Instantiate("CowBoy", spawnPosition, spawnRotation);
        isSpawned[spawnIdx] = true;
        Debug.Log("dd");
        
        // 아이템 임시 스폰 - 삭제 필요
        PhotonNetwork.Instantiate("Item/Cube", spawnPosition - new Vector3(2, 0, 0), spawnRotation);
        PhotonNetwork.Instantiate("Item/Cube", spawnPosition - new Vector3(5, 0, 0), spawnRotation);
    }
    
    int GetAvailableSpawnIndex(int playerIndex)
    {
        int availableIndex = -1;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!isSpawned[i])
            {
                availableIndex = i;
                break;
            }
        }

        return availableIndex;
    }
}
