using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	private Weapon currentWeapon;
	private Weapon triggeredWeapon;
	public Transform handTransform;

	private void OnEnable()
	{
		InputManager_Lobby.OnTryPickUpWeapon += PickupWeapon;
	}

	private void OnDisable()
	{
		InputManager_Lobby.OnTryPickUpWeapon -= PickupWeapon;
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
		// Ʈ���ŵ� �ѱⰡ �����ϰ� ���� ���Ⱑ �ִٸ� ���� �ѱ� �ı�
		if (triggeredWeapon == null)
		{
			return;
		}
		if (currentWeapon != null)
		{
			Destroy(currentWeapon.gameObject); // ���� �ѱ� �ı�
		}
		currentWeapon = triggeredWeapon;
		currentWeapon.transform.SetParent(handTransform);
		currentWeapon.transform.localPosition = Vector3.zero; 
		currentWeapon.transform.localRotation = Quaternion.identity;
		WeaponSpawner.Instance.RespawnWeapon(triggeredWeapon.spawnPointTransform);
		triggeredWeapon = null;
	}
}
