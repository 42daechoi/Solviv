﻿using UnityEngine;


    public enum RayType
    {
        RandomShot,
        KnifeStab
    }
    
    [CreateAssetMenu(fileName = "NewRayModule", menuName = "ScriptableObjects/RayModule")]
public class RayModule : ScriptableObject
{
    [Header("레이 동작 타입")]
    public RayType rayType;

    [Header("데미지")]
    public float damage = 10f;

    [Header("사격 랜덤 범위"), Tooltip("Speed가 0이 아닐 때 사격 각도 무작위 범위를 조절")]
    public float randomRange = 2f;

    public RaycastHit? ExecuteRayAction(Transform shooterTransform, float speed)
    {
        switch (rayType)
        {
            case RayType.RandomShot:
                return PerformRandomShot(shooterTransform, speed);
            case RayType.KnifeStab:
                return PerformShortStab(shooterTransform);
        }

        return null;
    }

    private RaycastHit? PerformRandomShot(Transform shooterTransform, float speed)
    {
        // 1) 카메라 정중앙에서 레이를 얻는다.
        //    (화면 중앙 픽셀(Screen.width / 2, Screen.height / 2) 기준)
        Ray centerRay = Camera.main.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2, 0)
        );
    
        // 레이 시작점(origin)과 기본 방향(direction)
        Vector3 origin = centerRay.origin;
        Vector3 direction = centerRay.direction;

        // 2) 이동 속도가 0이 아니라면, 랜덤 각도 적용
        if (!Mathf.Approximately(speed, 0f))
        {
            float randX = Random.Range(-randomRange, randomRange);
            float randY = Random.Range(-randomRange, randomRange);
        
            // direction에 랜덤 회전을 곱해줌
            direction = Quaternion.Euler(randX, randY, 0f) * direction;
        }

        // 3) 최종 레이 생성
        Ray finalRay = new Ray(origin, direction);

        // 4) 레이캐스트
        if (Physics.Raycast(finalRay, out RaycastHit hit, 100f))
        {
            // 디버그 레이 (씬 뷰에서 빨간 선으로 확인)
            Debug.DrawRay(origin, direction * 100f, Color.red, 1f);

            // 맞은 대상이 PlayerHealth를 가지고 있다면 데미지 적용
            PlayerHealth targetHealth = hit.collider.GetComponent<PlayerHealth>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
            return hit;
        }
        return null;
    }


    private RaycastHit? PerformShortStab(Transform shooterTransform)
    {

        Debug.Log("1");
        float knifeRange = 2f;

        // 1) 화면 중앙 기준 레이
        Ray centerRay = Camera.main.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2, 0)
        );

        // 2) 레이 생성 (origin/direction)
        Vector3 origin = centerRay.origin;
        Vector3 direction = centerRay.direction;
        Ray finalRay = new Ray(origin, direction);

        // 3) 레이캐스트 (knifeRange 까지만)
        if (Physics.Raycast(finalRay, out RaycastHit hit, knifeRange))
        {
            // 디버그 레이 (씬 뷰에서 파란 선으로 확인)
            Debug.DrawRay(origin, direction * knifeRange, Color.blue, 1f);

            // 맞은 대상이 PlayerHealth를 가지고 있다면 데미지 적용 - 이건 없애는게 좋을 듯 여러군데에서 ray를 재사용하려면
            // PlayerHealth targetHealth = hit.collider.GetComponent<PlayerHealth>();
            // if (targetHealth != null)
            // {
            //     targetHealth.TakeDamage(damage);
            // }
            return hit;
        }
        return null;
    }
}
