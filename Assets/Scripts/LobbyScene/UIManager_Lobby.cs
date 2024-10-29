using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager_Lobby : MonoBehaviour
{
    public TextMeshProUGUI readyGuideText;


    private void Start()
    {
        readyGuideText.text = "Press E to ready";
    }

    private void OnEnable()
    {
        LobbyManager.OnChangedReady += ChangeText;
    }

    private void OnDisable()
    {
        LobbyManager.OnChangedReady -= ChangeText;
    }

    private void ChangeText(bool isReady)
    {
        if (isReady)
        {
            readyGuideText.text = "Press E to Unready";
        }
        else
        {
            readyGuideText.text = "Press E to ready";
        }
    }
}
