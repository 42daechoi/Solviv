using System;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class Computer : MonoBehaviourPun, IInteractableObject
{
    [SerializeField] private CinemachineVirtualCamera moniterCamera;
    [SerializeField] private Canvas moniterCanvas;
    [SerializeField] private Transform interactionPoint;
    
    private bool IsAllGeneratorsActivated;
    private bool OnInteraction;

    private void OnEnable()
    {
        if (EventManager_Game.Instance != null)
        {
            EventManager_Game.Instance.OnAllGeneratorsActivated += HandleAllGeneratorsActivated;
            EventManager_Game.Instance.OnExitComputer += ForceExit;
        }
    }

    private void OnDisable()
    {
        EventManager_Game.Instance.OnAllGeneratorsActivated -= HandleAllGeneratorsActivated;
        EventManager_Game.Instance.OnExitComputer -= ForceExit;
    }

    void Start()
    {
        EventManager_Game.Instance.OnAllGeneratorsActivated += HandleAllGeneratorsActivated;
        EventManager_Game.Instance.OnExitComputer += ForceExit;
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
            moniterCanvas.gameObject.SetActive(true);
            moniterCamera.Priority = 20;
            OnInteraction = true;
        }

        Vector3 worldPosition = transform.TransformPoint(interactionPoint.localPosition);
        
        EventManager_Game.Instance.InvokeMoveToComputer(playerId, worldPosition);
        EventManager_Game.Instance.InvokeUseComputer(OnInteraction);
    }

    private void ForceExit()
    {
        Debug.Log("컴퓨터 강제 종료");

        moniterCamera.Priority = 5;
        moniterCamera.gameObject.SetActive(false);
        moniterCanvas.gameObject.SetActive(false);
        OnInteraction = false;
        if (EventManager_Game.Instance != null)
        {
            Debug.Log("이벤트 매니저 호출 성공");
            EventManager_Game.Instance.InvokeUseComputer(OnInteraction);
        }
    }

    private void HandleAllGeneratorsActivated()
    {
        IsAllGeneratorsActivated = true;
        Debug.Log("컴퓨터 상호작용 활성화.");
    }
}
