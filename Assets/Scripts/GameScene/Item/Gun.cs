using UnityEngine;

namespace GameScene.Item
{
    [CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun")]
    public class Gun : global::Item
    {
        [Header("총 레이 모듈")]
        public RayModule rayModule;

        [Header("현재 이동 스피드 (예시용)")]
        public float currentSpeed;

        private RaycastHit hit;
        public override void UseItem()
        {
            Transform shooterTransform = GetShooterTransform();

            if (rayModule != null && shooterTransform != null)
            {
                RaycastHit? raycastHit = rayModule.ExecuteRayAction(shooterTransform, currentSpeed);
            }
            else
            {
                Debug.LogWarning($"RayModule 또는 ShooterTransform이 유효하지 않습니다.");
            }
        }

        private Transform GetShooterTransform()
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                return playerObj.transform;
            return null;
        }
    }
}