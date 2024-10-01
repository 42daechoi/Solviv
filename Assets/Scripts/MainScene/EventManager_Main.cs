using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager_Main : MonoBehaviour
{
    public static event Action OnFindGameClicked;
    public static event Action OnCustomGameClicked;
    public static event Action OnSelectClicked;
    public static event Action OnOptionClicked;
    public static event Action OnQuitClicked;

     public void OnClickFindGame() {
        OnFindGameClicked?.Invoke();
    }
    public void OnClickCustomGame(){
        OnCustomGameClicked?.Invoke();
    }
    public void OnClickselect(){
        OnSelectClicked?.Invoke();
    }
    public void OnClickOption(){
        OnOptionClicked?.Invoke();
    }
    public void OnClickQuit(){
        OnQuitClicked?.Invoke();
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
