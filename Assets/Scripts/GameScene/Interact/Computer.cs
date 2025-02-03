using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class Computer : MonoBehaviourPun, IInteractableObject
{
    [SerializeField] private CinemachineVirtualCamera moniterCamera;
    private bool OnInteraction;

    void Start()
    {
        OnInteraction = false;
        moniterCamera.gameObject.SetActive(false);
    }
    public void Interact(int playerId)
    {
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
    }
}
