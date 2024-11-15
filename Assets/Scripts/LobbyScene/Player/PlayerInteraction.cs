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
        if (!photonView.IsMine) return;
        
        if (triggeredWeapon == null)
        {
            return;
        }
        if (currentWeapon != null)
        {
            Debug.Log(currentWeapon);
            PhotonNetwork.Destroy(currentWeapon.gameObject);
            currentWeapon = null;
        }

        
        PhotonView weaponPhotonView = triggeredWeapon.GetComponent<PhotonView>();
        if (weaponPhotonView != null && weaponPhotonView.Owner != PhotonNetwork.LocalPlayer)
        {
            weaponPhotonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }

        
        currentWeapon = triggeredWeapon;
        currentWeapon.transform.SetParent(handTransform);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;

        
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
                if (currentWeapon != null)
                {
                    Destroy(currentWeapon.gameObject);
                    Debug.Log("Previous weapon destroyed on remote client");
                }
                currentWeapon = weapon;
                currentWeapon.transform.SetParent(handTransform);
                currentWeapon.transform.localPosition = Vector3.zero;
                currentWeapon.transform.localRotation = Quaternion.identity;
            }
        }
    }

    private void Shoot()
    {
        if (!photonView.IsMine) return;
        if (currentWeapon != null)
        {
            currentWeapon.Shoot();
        }
    }

    private void Reload()
    {
        if (!photonView.IsMine) return;
        if (currentWeapon != null)
        {
            currentWeapon.Reload();
        }
    }
}
