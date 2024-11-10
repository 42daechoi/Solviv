using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    public int poolSize = 30;
    private Queue<GameObject> poolQueue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        poolQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject go = PhotonNetwork.Instantiate("Bullet", Vector3.zero, Quaternion.identity);
            go.SetActive(false);
            poolQueue.Enqueue(go);
        }
    }

    public GameObject GetBulletFromPool(Vector3 position, Quaternion rotation)
    {
        GameObject go;

        // 풀에서 총알을 꺼냄
        if (poolQueue.Count > 0)
        {
            go = poolQueue.Dequeue();
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.SetActive(true);
        }
        else
        {
            // 풀에 총알이 없으면 새로 생성
            go = PhotonNetwork.Instantiate("Bullet", position, rotation);
        }

        // 로컬 플레이어만 위치 및 회전 정보를 동기화
        PhotonView photonView = go.GetComponent<PhotonView>();
        if (photonView != null && photonView.IsMine)
        {
            // 로컬에서 생성한 총알만 동기화
            go.GetComponent<PhotonTransformView>().enabled = true;
        }

        return go;
    }

    public void ReturnBulletToPool(GameObject go)
    {
        go.SetActive(false);
        poolQueue.Enqueue(go);
    }
}
