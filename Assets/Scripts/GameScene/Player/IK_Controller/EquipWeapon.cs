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
    
    private bool isGunEquipped = false;
    
    private void Update()
    {
        // 테스트: G 키로 권총 즉시 잡기
        if (Input.GetKeyDown(KeyCode.G))
        {
            EquipGunInstantly();
        }
    }
    
    public void EquipGunInstantly()
    {
        // 권총 활성화
        gun.SetActive(true);

        // 오른손 타겟 위치 및 회전 설정
        rightHandTarget.transform.position = rightHandGrip.position;
        rightHandTarget.transform.rotation = rightHandGrip.rotation;

        // 왼손 타겟 위치 및 회전 설정
        leftHandTarget.transform.position = leftHandGrip.position;
        leftHandTarget.transform.rotation = leftHandGrip.rotation;

        isGunEquipped = true;

        Debug.Log("손 타겟이 설정된 위치로 이동했습니다.");
    }
}
