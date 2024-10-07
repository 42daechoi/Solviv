using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class PlayerSpawn : MonoBehaviour
{
    public Transform redSpawnPoint;
    public Transform blueSpawnPoint;
    public string prefabAddress = "PicoChan";
    private bool isSpawning = false;
    private PhotonView pv;

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
            GameObject redPlayer = PhotonNetwork.Instantiate(prefabAddress, redSpawnPoint.position, redSpawnPoint.rotation);
            //GameObject bluePlayer = Instantiate(handle.Result, blueSpawnPoint.position, blueSpawnPoint.rotation);

            PhotonView redPhotonView = redPlayer.GetComponent<PhotonView>();
            //PhotonView bluePhotonView = bluePlayer.GetComponent<PhotonView>();

            if (redPhotonView != null)
            {
                redPhotonView.TransferOwnership(PhotonNetwork.LocalPlayer); // 현재 플레이어에게 소유권 부여
            }
            redPlayer.GetComponent<PlayerMovement>();
            // bluePlayer.GetComponent<PlayerMovement>();
        }
        else
        {
            Debug.LogError("PicoChan 프리팹 로딩 실패");
        }

        isSpawning = false;
    }

}
