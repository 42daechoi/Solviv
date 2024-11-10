using System.Collections;
using Photon.Pun;
using UnityEngine;

public class WeaponSpawner : MonoBehaviourPun
{
    public static WeaponSpawner Instance;
    public GameObject[] spawnPoints;
    public GameObject specialSpawnPoint;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        InitSpawnWeapons();
    }

    void InitSpawnWeapons()
    {
        foreach (GameObject spawnPoint in spawnPoints)
        {
            SpawnRandomWeapon(spawnPoint.transform);
        }
        SpawnSpecialWeapon();
    }

    void SpawnRandomWeapon(Transform spawnPointTransform)
    {
        string randomWeapon = RandomWeaponName();
        if (randomWeapon == null)
        {
            Debug.LogError("RandomWeaponName() is null.");
            return;
        }

        GameObject go = PhotonNetwork.Instantiate(randomWeapon, spawnPointTransform.position, spawnPointTransform.rotation);
        if (go != null)
        {
            Weapon weapon = go.GetComponent<Weapon>();
            weapon.spawnPointTransform = spawnPointTransform;
        }
    }

    private string RandomWeaponName()
    {
        int randomIndex = Random.Range(0, 2);
        return randomIndex == 0 ? "Weapon1" : "Weapon2";
    }

    private void SpawnSpecialWeapon()
    {
        PhotonNetwork.Instantiate("SpecialWeapon", specialSpawnPoint.transform.position, specialSpawnPoint.transform.rotation);
    }

    public void RespawnWeapon(Transform spawnPointTransform)
    {
        // photonView 사용하여 RPC 호출
        photonView.RPC("RPC_RespawnWeapon", RpcTarget.All, spawnPointTransform.position, spawnPointTransform.rotation);
    }

    [PunRPC]
    private void RPC_RespawnWeapon(Vector3 position, Quaternion rotation)
    {
        StartCoroutine(RespawnWeaponAfterDelay(position, rotation));
    }

    IEnumerator RespawnWeaponAfterDelay(Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(3);

        string randomWeapon = RandomWeaponName();
        if (randomWeapon != null)
        {
            PhotonNetwork.Instantiate(randomWeapon, position, rotation);
        }
    }
}
