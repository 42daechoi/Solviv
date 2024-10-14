using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
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

    void SpawnPlayer()
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
            GameObject redPlayer = PhotonNetwork.Instantiate("PicoChan", spawnPosition, Quaternion.identity);
            PhotonView pv = redPlayer.GetComponent<PhotonView>();

            if (pv != null)
            {
                pv.TransferOwnership(PhotonNetwork.LocalPlayer); // ���� �÷��̾�� ������ �ο�
            }
            Debug.Log("aa");
        }
        else
        {
            Debug.LogWarning("�浹 ���� ������ �� �ִ� ��ġ�� ã�� ���߽��ϴ�.");
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
