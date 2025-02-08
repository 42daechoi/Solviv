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
            Debug.Log($"������Ʈ ������: {itemName}, ���� ����: {pool[itemName].Count}");
            return obj;
        }
        Debug.Log("������Ʈ Ǯ�� �ش� �������� �����ϴ�.");
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
        Debug.Log("������Ʈ Ǯ�� �ش� �������� �����ϴ�.");
        return null;
    }

    public void ReturnObject(GameObject obj, string itemName)
    {
        Debug.Log("1");
        if (!pool.ContainsKey(itemName))
        {
            pool[itemName] = new Queue<GameObject>();
            Debug.LogWarning($"{itemName}�� ���� Ǯ�� ã�� ���߽��ϴ�. ���ο� Ǯ�� �����մϴ�.");
        }
        Debug.Log("2");
        obj.SetActive(false);
        pool[itemName].Enqueue(obj);
        Debug.Log($"������Ʈ ��ȯ��: {itemName}, ���� Ǯ ����: {pool[itemName].Count}");
        Debug.Log("3");
    }
}
