using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform spawnPointTransform;

    public string weaponName;
    public float knockbackPower;
    public int bulletCount;
    public int maxBulletCount;
    public Vector3 positionOffset;
    public float bulletSpeed;

    public void Shoot()
    {
        if (bulletCount > 0)
        {
            bulletCount--;
            Vector3 bulletPosition = transform.position + transform.forward * positionOffset.z
                                     + transform.right * positionOffset.x
                                     + transform.up * positionOffset.y;

            PhotonView photonView = gameObject.GetComponent<PhotonView>();
            photonView.RPC("RPC_SpawnBullet", RpcTarget.All, bulletPosition, spawnPointTransform.rotation, knockbackPower, bulletSpeed);
        }
    }

    [PunRPC]
    public void RPC_SpawnBullet(Vector3 position, Quaternion rotation, float knockbackPower, float bulletSpeed)
    {
        GameObject bullet = PoolManager.Instance.GetBulletFromPool(position, rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.knockbackPower = knockbackPower;

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = spawnPointTransform.forward * bulletSpeed;
        }
    }

    public void Reload()
    {
        bulletCount = maxBulletCount;
    }
}
