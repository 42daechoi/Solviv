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
    public static event Action OnSoloModeClicked;
    public static event Action OnMultiModeClicked;
    public static event Action OnControlPanelButtonClicked;
    public static event Action OnResolutionPanelButtonClicked;
    public static event Action OnAudioPanelButtonClicked;
    public static event Action OnOptionConfirmButtonClicked;

     public void OnClickFindGame() {
        OnFindGameClicked?.Invoke();   
        Debug.Log("OnClickFindGame 호출됨");
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
    public void OnClickControlPanelButton(){
        OnControlPanelButtonClicked?.Invoke();
        Debug.Log("컨트롤 패널");
    }
    public void OnClickResolutionPanelButton(){
        OnResolutionPanelButtonClicked?.Invoke();
        Debug.Log("해상도 패널");
    }
    public void OnClickAudioPanelButton(){
        OnAudioPanelButtonClicked?.Invoke();
        Debug.Log("오디오 패널");
    }
    public void OnOptionConfirmButton(){
        OnOptionConfirmButtonClicked?.Invoke();
        Debug.Log("설정 적용");
    }

    public void OnClickQuit(){
        OnQuitClicked?.Invoke();
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void OnClickSoloMode() {
        OnSoloModeClicked?.Invoke();
        
    }
    
    public void OnClickMultiMode() {
        OnMultiModeClicked?.Invoke();
        
    }
}
