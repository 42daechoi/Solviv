using System.Collections;
using Photon.Pun;
using UnityEngine;

public class PlayerInteraction : MonoBehaviourPun
{
    private Weapon currentWeapon;
    private Weapon triggeredWeapon;
    public Transform handTransform;

    private void OnEnable()
    {
        InputManager_Lobby.OnTryPickUpWeapon += PickupWeapon;
        InputManager_Lobby.OnPlayerReload += Reload;
        InputManager_Lobby.OnPlayerShoot += Shoot;
    }

    private void OnDisable()
    {
        InputManager_Lobby.OnTryPickUpWeapon -= PickupWeapon;
        InputManager_Lobby.OnPlayerReload -= Reload;
        InputManager_Lobby.OnPlayerShoot -= Shoot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            triggeredWeapon = other.GetComponent<Weapon>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            if (triggeredWeapon != null && other.GetComponent<Weapon>() == triggeredWeapon)
            {
                triggeredWeapon = null;
            }
        }
    }

    private void PickupWeapon()
    {
        if (triggeredWeapon == null)
        {
            return;
        }
        if (currentWeapon != null)
        {
            PhotonNetwork.Destroy(currentWeapon.gameObject);
        }

        // 소유권 이전
        PhotonView weaponPhotonView = triggeredWeapon.GetComponent<PhotonView>();
        if (weaponPhotonView != null && weaponPhotonView.Owner != PhotonNetwork.LocalPlayer)
        {
            weaponPhotonView.TransferOwnership(PhotonNetwork.LocalPlayer); // 현재 플레이어에게 소유권 부여
        }

        // 새로운 무기를 현재 플레이어에게 설정
        currentWeapon = triggeredWeapon;
        currentWeapon.transform.SetParent(handTransform);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;

        // RPC를 사용하여 다른 플레이어에게 무기 줍기 동작을 알림
        photonView.RPC("RPC_PickupWeapon", RpcTarget.OthersBuffered, currentWeapon.GetComponent<PhotonView>().ViewID);

        triggeredWeapon = null;
    }

    [PunRPC]
    private void RPC_PickupWeapon(int weaponViewID)
    {
        PhotonView weaponPhotonView = PhotonView.Find(weaponViewID);
        if (weaponPhotonView != null)
        {
            Weapon weapon = weaponPhotonView.GetComponent<Weapon>();
            if (weapon != null)
            {
                currentWeapon = weapon;
                currentWeapon.transform.SetParent(handTransform);
                currentWeapon.transform.localPosition = Vector3.zero;
                currentWeapon.transform.localRotation = Quaternion.identity;
            }
        }
    }

    private void Shoot()
    {
        Debug.Log("shoot");
        if (currentWeapon != null)
        {
            currentWeapon.Shoot();
        }
    }

    private void Reload()
    {
        Debug.Log("reload");
        if (currentWeapon != null)
        {
            currentWeapon.Reload();
        }
    }
}
