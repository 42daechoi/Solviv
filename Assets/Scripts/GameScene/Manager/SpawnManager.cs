using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpawnManager : MonoBehaviourPun
{
	public Transform[] playerSpawnPoints;
	public Transform[] itemSpawnPoints;
	private bool[] isSpawned;

	void Start()
	{
		isSpawned = new bool[playerSpawnPoints.Length];
		SpawnPlayers();
		if (PhotonNetwork.IsMasterClient)
		{
            SpawnItems();
        }
	}

	void SpawnPlayers()
	{
		int playerIdx = PhotonNetwork.LocalPlayer.ActorNumber - 1;
		int spawnIdx = GetAvailableSpawnIndex(playerIdx);

		if (spawnIdx < 0) return;
		Vector3 spawnPosition = playerSpawnPoints[spawnIdx].position;
		Quaternion spawnRotation = playerSpawnPoints[spawnIdx].rotation;
		GameObject player = PhotonNetwork.Instantiate("CowBoy", spawnPosition, spawnRotation);
        Debug.Log("Player spawned: " + player.name + " for player: " + PhotonNetwork.LocalPlayer.NickName);
        photonView.RPC("UsedSpawnPointSync", RpcTarget.All, spawnIdx);
		
		// 아이템 임시 스폰 - 삭제 필요
		PhotonNetwork.Instantiate("Item/Battery", spawnPosition - new Vector3(2, 2, 0), spawnRotation);
		PhotonNetwork.Instantiate("Item/Battery", spawnPosition - new Vector3(5, 2, 0), spawnRotation);
		PhotonNetwork.Instantiate("Item/Battery", spawnPosition - new Vector3(4, 2, 0), spawnRotation);
	}
	
	private int GetAvailableSpawnIndex(int playerIdx)
	{
        if (playerIdx >= 0 && playerIdx < playerSpawnPoints.Length && !isSpawned[playerIdx])
        {
            return playerIdx;
        }

        for (int i = 0; i < playerSpawnPoints.Length; i++)
        {
            if (!isSpawned[i])
            {
                return i;
            }
        }

        return -1;
    }

	[PunRPC]
	private void UsedSpawnPointSync(int spawnIdx)
	{
        isSpawned[spawnIdx] = true;
    }

	void SpawnItems()
	{
		//SpawnPasswordPapers();
	}

	private void SpawnPasswordPapers()
	{
		PasswordGenerator passwordGenerator = GameManager.Instance.GetPasswordGenerator();
		List<string> passwords = passwordGenerator.GetAllPasswords();

		for (int i = 0; i < 10; i++)
		{
			//GameObject go = PhotonNetwork.Instantiate("Item/PasswordPaper", Vector3.zero, Quaternion.Euler);
		   // FarmingObject fo = go.GetComponent<FarmingObject>();
			PasswordPaper paper = ScriptableObject.CreateInstance<PasswordPaper>();
			paper.SetPassword(passwords[i]);
			//fo.item = paper;
		}
	}
}
