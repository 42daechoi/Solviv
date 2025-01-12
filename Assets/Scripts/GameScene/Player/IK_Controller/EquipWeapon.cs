using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    
    [SerializeField] private GameObject leftHandTarget;
    [SerializeField] private GameObject rightHandTarget;
    [SerializeField] private GameObject gun;
    [SerializeField] private Transform leftHandGrip;
    [SerializeField] private Transform rightHandGrip;
    [SerializeField] private Transform firstPersonCamera;
    
    [SerializeField] private Vector3 gunPositionOffset = new Vector3(0, -0, 0);
    [SerializeField] private Vector3 gunRotationOffset = Vector3.zero;
    
    private bool isGunEquipped = false;
    
    private void Update()
    {
        // 테스트: G 키로 권총 즉시 잡기
        if (Input.GetKeyDown(KeyCode.G))
        {
            EquipGunInstantly();
        }
        
        if (isGunEquipped)
        {
            SyncTargetWithGrip(rightHandTarget.transform, rightHandGrip);
            SyncTargetWithGrip(leftHandTarget.transform, leftHandGrip);
            
            SyncGunWithCamera();
        }
    }
    
    public void EquipGunInstantly()
    {
        gun.SetActive(true);
        
        rightHandTarget.transform.position = rightHandGrip.position;
        rightHandTarget.transform.rotation = rightHandGrip.rotation;
        
        leftHandTarget.transform.position = leftHandGrip.position;
        leftHandTarget.transform.rotation = leftHandGrip.rotation;

        isGunEquipped = true;
    }
    
    private void SyncTargetWithGrip(Transform target, Transform grip)
    {
        target.position = grip.position;
        target.rotation = grip.rotation;
    }
    
    private void SyncGunWithCamera()
    {
        gun.transform.position = firstPersonCamera.position +
                                 firstPersonCamera.forward * gunPositionOffset.z +
                                 firstPersonCamera.right * gunPositionOffset.x +
                                 firstPersonCamera.up * gunPositionOffset.y;
        
        gun.transform.rotation = firstPersonCamera.rotation * Quaternion.Euler(gunRotationOffset);
    }
}
