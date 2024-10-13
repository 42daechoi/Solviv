using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager_Custom : MonoBehaviour
{
    public static event Action OnCreateButtonClicked;

    public static event Action OnJoinButonClicked;

    public static event Action OnSearchButtonClicked;

    public static event Action OnPreviousButtonClicked;

    public void OnClickCreateButton() {
        OnCreateButtonClicked?.Invoke();   
    }

    public void OnClickJoinButton(){
        OnJoinButonClicked?.Invoke();
    }

    public void OnClickSearchButton(){
        OnSearchButtonClicked?.Invoke();
    }
    public void OnPreviousButton(){
        OnPreviousButtonClicked?.Invoke();
    }
}
