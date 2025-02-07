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
        }
        Debug.Log("2");
        obj.SetActive(false);
        pool[itemName].Enqueue(obj);
        Debug.Log("3");
    }
}
