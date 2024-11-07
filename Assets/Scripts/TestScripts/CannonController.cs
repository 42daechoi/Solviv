using System.Collections;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject cannonballPrefab;   // 대포알 프리팹
    public Transform firePoint;           // 대포 발사 위치
    public float launchForce = 500f;      // 대포 발사 힘

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireCannon();
        }
    }

    void FireCannon()
    {
        // 마우스 클릭 위치를 월드 좌표로 변환
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // 대포알 생성 및 발사
            GameObject cannonball = Instantiate(cannonballPrefab, firePoint.position, Quaternion.identity);
            Rigidbody rb = cannonball.GetComponent<Rigidbody>();

            // 목표 방향 계산
            Vector3 direction = (hit.point - firePoint.position).normalized;

            // 대포알에 힘을 가해 발사
            rb.AddForce(direction * launchForce);
        }
    }
}