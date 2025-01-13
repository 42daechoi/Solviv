using UnityEngine;

namespace GameScene.Item
{
    [CreateAssetMenu(fileName = "Knife", menuName = "ScriptableObjects/Knife")]
    public class Knife : global::Item
    {
        [Header("칼 레이 모듈")]
        public RayModule rayModule;

        public override void UseItem()
        {
            Transform shooterTransform = GetShooterTransform();

            if (rayModule != null && shooterTransform != null)
            {
                rayModule.ExecuteRayAction(shooterTransform, 0f);
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