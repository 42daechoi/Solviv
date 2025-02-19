using UnityEngine;

[CreateAssetMenu(fileName = "Battery", menuName = "ScriptableObjects/Battery")]
public class Battery : Item
{
    [SerializeField] private RayModule rayModule;
    public override void UseItem()
    {
        Transform shooterTransform = GetShooterTransform();
        if (shooterTransform == null)
        {
            Debug.Log("Battery : 플레이어 트랜스폼을 찾지 못했습니다.");
            return;
        }

        if (rayModule != null && shooterTransform != null)
        {
            RaycastHit? raycastHit = rayModule.ExecuteRayAction(shooterTransform, 0f);
            if (raycastHit.HasValue)
            {
                Generator generator = raycastHit.Value.collider.GetComponent<Generator>();
                if (generator != null)
                {
                    HeldItem heldItem = shooterTransform.gameObject.GetComponent<HeldItem>();
                    generator.TryInstallBattery(heldItem);
                }
            }
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
