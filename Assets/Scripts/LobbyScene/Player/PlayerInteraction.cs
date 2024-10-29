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
		// 트리거된 총기가 존재하고 현재 무기가 있다면 기존 총기 파괴
		if (triggeredWeapon == null)
		{
			return;
		}
		if (currentWeapon != null)
		{
			Destroy(currentWeapon.gameObject); // 기존 총기 파괴
		}
		currentWeapon = triggeredWeapon;
		currentWeapon.transform.SetParent(handTransform);
		currentWeapon.transform.localPosition = Vector3.zero; 
		currentWeapon.transform.localRotation = Quaternion.identity;
		WeaponSpawner.Instance.RespawnWeapon(triggeredWeapon.spawnPointTransform);
		triggeredWeapon = null;
	}
}
