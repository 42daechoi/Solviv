using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject specialSpawnPoint;

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
        PhotonNetwork.Instantiate(randomWeapon, spawnPointTransform.position, spawnPointTransform.rotation);
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

    public void RespawnWeapon(GameObject weapon)
    {
        // 플레이어가 무기를 주웠을 경우 호출 필요
        StartCoroutine(RespawnWeaponAfterDelay(weapon.transform));
    }

    IEnumerator RespawnWeaponAfterDelay(Transform spawnPointTransform)
    {
        yield return new WaitForSeconds(3);
        SpawnRandomWeapon(spawnPointTransform);
    }
}
