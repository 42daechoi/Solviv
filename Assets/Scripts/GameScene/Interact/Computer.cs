using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class Computer : MonoBehaviourPun, IInteractableObject
{
    [SerializeField] private CinemachineVirtualCamera moniterCamera;
    private bool IsAllGeneratorsActivated;
    private bool OnInteraction;

    private void OnEnable()
    {
        if (EventManager_Game.Instance != null)
        {
            EventManager_Game.Instance.OnAllGeneratorsActivated += HandleAllGeneratorsActivated;
        }
    }

    private void OnDisable()
    {
        EventManager_Game.Instance.OnAllGeneratorsActivated -= HandleAllGeneratorsActivated;
    }

    void Start()
    {
        if (EventManager_Game.Instance != null)
        {
            EventManager_Game.Instance.OnAllGeneratorsActivated += HandleAllGeneratorsActivated;
        }
        IsAllGeneratorsActivated = false;
        OnInteraction = false;
        moniterCamera.gameObject.SetActive(false);
    }
    public void Interact(int playerId)
    {
        Debug.Log($"Computer : {IsAllGeneratorsActivated}");
        if (IsAllGeneratorsActivated == false)
        {
            return;
        }
        if (!OnInteraction)
        {
            moniterCamera.gameObject.SetActive(true);
            moniterCamera.Priority = 20;
            OnInteraction = true;
        }
        else
        {
            moniterCamera.Priority = 5;
            moniterCamera.gameObject.SetActive(false);
            OnInteraction = false;
        }
        EventManager_Game.Instance.InvokeUseComputer(OnInteraction);
    }

    private void HandleAllGeneratorsActivated()
    {
        IsAllGeneratorsActivated = true;
        Debug.Log("컴퓨터 상호작용 활성화.");
    }
}
