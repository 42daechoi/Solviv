using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CustomUI_Event : MonoBehaviour
{
    void OnEnable(){
        EventManager_Custom.OnCreateButtonClicked += Create;
        EventManager_Custom.OnJoinButonClicked += Join;
        EventManager_Custom.OnSearchButtonClicked += Search;
        EventManager_Custom.OnPreviousButtonClicked += Previous;
    }
    void OnDisable(){
        EventManager_Custom.OnCreateButtonClicked -= Create;
        EventManager_Custom.OnJoinButonClicked -= Join;
        EventManager_Custom.OnSearchButtonClicked -= Search;
        EventManager_Custom.OnPreviousButtonClicked -= Previous;
    }

    void Create(){
        //Create 버튼 로직 구현
        Debug.Log("create버튼");
    }

    void Join(){
        //Join 버튼 로직 구현 필요
        Debug.Log("join버튼");
    }
    void Search(){
        Debug.Log("search버튼");
        //Search 버튼 로직 구현 필요
    }
    void Previous(){
        Debug.Log("previous버튼");
        SceneManager.LoadScene("MainScene");
    }
}
