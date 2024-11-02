using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class SpawnManager_Lobby : MonoBehaviour
{
	public Vector3 redSpawnAreaCenter;      // ������ ���� ������ �߽�
	public Vector3 blueSpawnAreaCenter;     // ����� ���� ������ �߽�
	public float spawnAreaRadius = 4f;      // ���� ������ ������
	public float checkRadius = 0.5f;        // �浹 ������ ���� ������
	public int maxAttempts = 10;            // ���� ��ġ�� ã�� ���� �ִ� �õ� Ƚ��

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
			// ���� ���� �ȿ��� ���� ��ġ ����
			spawnPosition = GetRandomPositionWithinArea();

			// �ش� ��ġ �ֺ��� �浹ü�� �ִ��� �˻�
			if (!Physics.CheckSphere(spawnPosition, checkRadius))
			{
				positionFound = true;
				break;  // �浹�� ������ ���� ��ġ�� Ȯ���ϰ� �ݺ� ����
			}
		}

		if (positionFound)
		{
			// �÷��̾ ����
			GameObject playerCharacter = PhotonNetwork.Instantiate("Default_Unit", spawnPosition, Quaternion.identity);
			StartCoroutine(SetCameraTarget(playerCharacter));
			PhotonView pv = playerCharacter.GetComponent<PhotonView>();

			if (pv != null)
			{
				pv.TransferOwnership(PhotonNetwork.LocalPlayer); // ���� �÷��̾�� ������ �ο�
			}
		}
		else
		{
			Debug.LogWarning("�浹 ���� ������ �� �ִ� ��ġ�� ã�� ���߽��ϴ�.");
		}
	}
	private IEnumerator SetCameraTarget(GameObject playerCharacter)
	{
		// ĳ���Ͱ� ������ Ȱ��ȭ�� ������ ���
		yield return new WaitUntil(() => playerCharacter != null);

		CinemachineVirtualCamera virtualCamera = playerCharacter.GetComponentInChildren<CinemachineVirtualCamera>();
		if (virtualCamera != null)
		{
            virtualCamera.gameObject.tag = "CinamachinCamera";
            Camera.main.tag = "MainCamera";
            //FindObjectOfType<Camera>().gameObject.SetActive(false);
            Debug.Log(Camera.main);

		}
		else
		{
			Debug.LogWarning("Cinemachine Virtual Camera�� ã�� �� �����ϴ�.");
		}
	}

	Vector3 GetRandomPositionWithinArea()
	{
		// ������ ��ġ�� ���� ���� ������ ����
		Vector3 randomPosition = redSpawnAreaCenter + Random.insideUnitSphere * spawnAreaRadius;
		randomPosition.y = redSpawnAreaCenter.y;  // y �� ����

		return randomPosition;
	}
}
