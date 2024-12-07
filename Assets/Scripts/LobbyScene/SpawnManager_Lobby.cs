using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class SpawnManager_Lobby : MonoBehaviour
{
	public Vector3 redSpawnAreaCenter;
	public Vector3 blueSpawnAreaCenter;
	public float spawnAreaRadius = 4f;    
	public float checkRadius = 0.5f;   
	public int maxAttempts = 10;          

	void Start()
	{
		redSpawnAreaCenter = new Vector3(-18, 150, 8);
		blueSpawnAreaCenter = new Vector3(-18, 10, 78);
		SpawnPlayer();
	}

	public void SpawnPlayer()
	{
		bool positionFound = false;
		Vector3 spawnPosition = Vector3.zero;

		for (int i = 0; i < maxAttempts; i++)
		{
			spawnPosition = GetRandomPositionWithinArea();
			if (!Physics.CheckSphere(spawnPosition, checkRadius))
			{
				positionFound = true;
				break;  
			}
		}

		if (positionFound)
		{
			GameObject playerCharacter = PhotonNetwork.Instantiate("CowBoy", spawnPosition, Quaternion.identity);
			StartCoroutine(SetCameraTarget(playerCharacter));
			PhotonView pv = playerCharacter.GetComponent<PhotonView>();

			if (pv != null)
			{
				pv.TransferOwnership(PhotonNetwork.LocalPlayer); 
			}
		}
		else
		{
			Debug.LogWarning("position not found");
		}
	}
	private IEnumerator SetCameraTarget(GameObject playerCharacter)
	{
		yield return new WaitUntil(() => playerCharacter != null);

		CinemachineVirtualCamera virtualCamera = playerCharacter.GetComponentInChildren<CinemachineVirtualCamera>();
		if (virtualCamera != null)
		{
            virtualCamera.gameObject.tag = "CinamachinCamera";
            Camera.main.tag = "MainCamera";
            Debug.Log(Camera.main);

		}
		else
		{
			Debug.LogWarning("Cinemachine Virtual Camera error.");
		}
	}

	Vector3 GetRandomPositionWithinArea()
	{
		Vector3 randomPosition = redSpawnAreaCenter + Random.insideUnitSphere * spawnAreaRadius;
		randomPosition.y = redSpawnAreaCenter.y;  // y �� ����

		return randomPosition;
	}
}
