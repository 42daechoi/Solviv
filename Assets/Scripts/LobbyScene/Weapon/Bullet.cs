using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviourPun
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
        // 총알이 충돌했을 때 비활성화 및 풀로 반환
        Deactivate();
    }

    private void Deactivate()
    {
        // RPC 호출하여 다른 플레이어들도 총알을 비활성화
        if (photonView.IsMine)
        {
            photonView.RPC("RPC_DeactivateBullet", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void RPC_DeactivateBullet()
    {
        Debug.Log("Deactivate");
        PoolManager.Instance.ReturnBulletToPool(gameObject);  // 풀로 반환
    }
}
