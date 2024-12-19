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
