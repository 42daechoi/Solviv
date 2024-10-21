using UnityEngine;

public interface IEventManager
{
    void HandleCommonEvents(string keyType, Vector3 moveDirection);
    void HandleSpecificEvents();
}