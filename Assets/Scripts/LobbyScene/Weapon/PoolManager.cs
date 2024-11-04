using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
        if (poolQueue.Count > 0)
        {
            GameObject go = poolQueue.Dequeue();
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.SetActive(true);
            return go;
        }
        else
        {
            GameObject go = PhotonNetwork.Instantiate("Bullet", position, rotation);
            return go;
        }
    }

    public void ReturnBulletToPool(GameObject go)
    {
        go.SetActive(false);
        poolQueue.Enqueue(go);
    }
}
