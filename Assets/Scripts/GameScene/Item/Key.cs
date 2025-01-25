using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Key : Item
{
    private RayModule rayModule;
    public float useDistance = 2f;

    public override void UseItem()
    {
        if (rayModule.TryGetShortRayHit(useDistance, out RaycastHit hit))
        {
            RotationDoor door = hit.collider.GetComponent<RotationDoor>();
            if (door != null)
            {
                // 아이템 사용이벤트 발행,
                // 파라미터로 'this' (즉, Key)를 넘김
                EventManager_Game.Instance.InvokeUseItemWithItem(this);
            }
        }
    }
}
