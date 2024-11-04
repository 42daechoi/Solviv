using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float lifetime = 3f;
    public float knockbackPower = 0;

    private void OnEnable()
    {
        Invoke("Deactivate", lifetime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Deactivate();
    }

    private void Deactivate()
    {
        Debug.Log("Deactivate");
        PoolManager.Instance.ReturnBulletToPool(gameObject);
        knockbackPower = 0;
    }
}
