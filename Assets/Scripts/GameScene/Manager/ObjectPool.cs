using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    private void Awake()
    {
        instance = this;
    }

    private Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();

    public GameObject GetObject(string itemName, Vector3 position, Quaternion rotation)
    {
        if (pool.ContainsKey(itemName) && pool[itemName].Count > 0)
        {
            GameObject obj = pool[itemName].Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            Debug.Log($"오브젝트 가져옴: {itemName}, 남은 개수: {pool[itemName].Count}");
            return obj;
        }
        Debug.Log("오브젝트 풀에 해당 아이템이 없습니다.");
        return null;
    }

    public GameObject GetObject(string itemName, Vector3 position)
    {
        if (pool.ContainsKey(itemName) && pool[itemName].Count > 0)
        {
            GameObject obj = pool[itemName].Dequeue();
            obj.transform.position = position;
            obj.SetActive(true);
            return obj;
        }
        Debug.Log("오브젝트 풀에 해당 아이템이 없습니다.");
        return null;
    }

    public void ReturnObject(GameObject obj, string itemName)
    {
        Debug.Log("1");
        if (!pool.ContainsKey(itemName))
        {
            pool[itemName] = new Queue<GameObject>();
            Debug.LogWarning($"{itemName}에 대한 풀을 찾지 못했습니다. 새로운 풀을 생성합니다.");
        }
        Debug.Log("2");
        obj.SetActive(false);
        pool[itemName].Enqueue(obj);
        Debug.Log($"오브젝트 반환됨: {itemName}, 현재 풀 개수: {pool[itemName].Count}");
        Debug.Log("3");
    }
}
