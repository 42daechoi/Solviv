using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public Transform[] playerSpawnPoints;
	public Transform[] itemSpawnPoints;
	private bool[] isSpawned;

	void Start()
	{
		isSpawned = new bool[playerSpawnPoints.Length];
		SpawnPlayers();
		SpawnItems();
	}

	void SpawnPlayers()
	{
		int playerIdx = PhotonNetwork.LocalPlayer.ActorNumber - 1;
		int spawnIdx = GetAvailableSpawnIndex(playerIdx);

		if (spawnIdx < 0) return;
		Vector3 spawnPosition = playerSpawnPoints[spawnIdx].position;
		Quaternion spawnRotation = playerSpawnPoints[spawnIdx].rotation;
		GameObject player = PhotonNetwork.Instantiate("CowBoy", spawnPosition, spawnRotation);
		isSpawned[spawnIdx] = true;
		
		// 아이템 임시 스폰 - 삭제 필요
		PhotonNetwork.Instantiate("Item/Gun", spawnPosition - new Vector3(2, 2, 0), spawnRotation);
		PhotonNetwork.Instantiate("Item/Key", spawnPosition - new Vector3(5, 2, 0), spawnRotation);
		PhotonNetwork.Instantiate("Item/Battery", spawnPosition - new Vector3(4, 2, 0), spawnRotation);
	}
	
	int GetAvailableSpawnIndex(int playerIndex)
	{
		int availableIndex = -1;

		for (int i = 0; i < playerSpawnPoints.Length; i++)
		{
			if (!isSpawned[i])
			{
				availableIndex = i;
				break;
			}
		}

		return availableIndex;
	}

	void SpawnItems()
	{
		SpawnPasswordPapers();
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
