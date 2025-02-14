using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int activeGeneratorCount;
    [SerializeField] private int maxGeneratorCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        activeGeneratorCount = 0;
        maxGeneratorCount = 1;
    }

    public void AddActiveGenerator()
    {
        activeGeneratorCount++;
        if (activeGeneratorCount >= maxGeneratorCount)
        {
            EventManager_Game.Instance.InvokeAllGeneratorsActivated();
            Debug.Log("모든 발전기 가동 완료.");
        }
    }
}
