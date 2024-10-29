using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
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
            Debug.LogError("RandomWeponName() is null.");
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
        if (randomIndex == 0)
            return "Weapon1";
        if (randomIndex == 1)
            return "Weapon2";
        return null;
    }

    private void SpawnSpecialWeapon()
    {
        PhotonNetwork.Instantiate("SpecialWeapon", specialSpawnPoint.transform.position, specialSpawnPoint.transform.rotation);
    }

    public void RespawnWeapon(Transform spawnPointTransform)
    {
        // 플레이어가 무기를 주웠을 경우 호출 필요
        StartCoroutine(RespawnWeaponAfterDelay(spawnPointTransform));
    }

    IEnumerator RespawnWeaponAfterDelay(Transform spawnPointTransform)
    {
        yield return new WaitForSeconds(3);
        SpawnRandomWeapon(spawnPointTransform);
    }
}
