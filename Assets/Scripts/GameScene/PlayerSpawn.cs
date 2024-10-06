using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerSpawn : MonoBehaviour
{
    public Transform redSpawnPoint;
    public Transform blueSpawnPoint;
    public string prefabAddress = "PicoChan";
    private bool isSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnPicoChan());
        }
    }
    private IEnumerator SpawnPicoChan()
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(prefabAddress);
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject redPlayer = Instantiate(handle.Result, redSpawnPoint.position, redSpawnPoint.rotation);
            
            GameObject bluePlayer = Instantiate(handle.Result, blueSpawnPoint.position, blueSpawnPoint.rotation);
            
            redPlayer.GetComponent<PlayerMovement>();
            bluePlayer.GetComponent<PlayerMovement>();
        }
        else
        {
            Debug.LogError("PicoChan 프리팹 로딩 실패");
        }

        isSpawning = false;
    }
}
