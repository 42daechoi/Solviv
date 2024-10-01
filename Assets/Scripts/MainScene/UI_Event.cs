using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    void OnEnable()
    {
        EventManager_Main.OnFindGameClicked += FindGame;
        EventManager_Main.OnCustomGameClicked += CustomGame;
        EventManager_Main.OnSelectClicked += SelectCharacter;
        EventManager_Main.OnOptionClicked += Option;
        EventManager_Main.OnQuitClicked += Quit;
    }

    void OnDisable()
    {
        EventManager_Main.OnFindGameClicked -= FindGame;
        EventManager_Main.OnCustomGameClicked -= CustomGame;
        EventManager_Main.OnSelectClicked -= SelectCharacter;
        EventManager_Main.OnOptionClicked -= Option;
        EventManager_Main.OnQuitClicked -= Quit;
    }

    void FindGame()
    {
        Debug.Log("FindGame 버튼 누름");
        // 새 게임 찾기 로직 구현 필요
    }

    void CustomGame()
    {
        Debug.Log("CustomGame 버튼 누름");
        // 커스텀 게임 목록 보기 로직 구현 필요
    }

    void SelectCharacter()
    {
        Debug.Log("Select 버튼 누름");
        // 캐릭터 설정 로직 구현 필요
    }

    void Option()
    {
        Debug.Log("Option 버튼 누름");
        // 환경 설정 로직 구현 필요
    }

    void Quit()
    {
        Debug.Log("Quit 버튼 누름");
    }
}
