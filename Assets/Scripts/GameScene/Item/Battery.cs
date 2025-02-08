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
            Debug.Log("Battery : �÷��̾� Ʈ�������� ã�� ���߽��ϴ�.");
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
            Debug.LogWarning($"RayModule �Ǵ� ShooterTransform�� ��ȿ���� �ʽ��ϴ�.");
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
