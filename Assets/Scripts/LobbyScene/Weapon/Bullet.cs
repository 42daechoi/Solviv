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
        // �Ѿ��� �浹���� �� ��Ȱ��ȭ �� Ǯ�� ��ȯ
        Deactivate();
    }

    private void Deactivate()
    {
        // RPC ȣ���Ͽ� �ٸ� �÷��̾�鵵 �Ѿ��� ��Ȱ��ȭ
        if (photonView.IsMine)
        {
            photonView.RPC("RPC_DeactivateBullet", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void RPC_DeactivateBullet()
    {
        Debug.Log("Deactivate");
        PoolManager.Instance.ReturnBulletToPool(gameObject);  // Ǯ�� ��ȯ
    }
}
