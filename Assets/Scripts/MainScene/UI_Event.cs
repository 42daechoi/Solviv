using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject GameModeSelect;
    public GameObject OptionUI;
    public GameObject ControlPanel;
    public GameObject resolutionPanel;
    public GameObject AudioPanel;
    void OnEnable()
    {
        EventManager_Main.OnFindGameClicked += FindGame;
        EventManager_Main.OnCustomGameClicked += CustomGame;
        EventManager_Main.OnSelectClicked += SelectCharacter;
        EventManager_Main.OnOptionClicked += Option;
        EventManager_Main.OnQuitClicked += Quit;
        EventManager_Main.OnSoloModeClicked += Solo;
        EventManager_Main.OnMultiModeClicked += Multi;
        EventManager_Main.OnControlPanelButtonClicked += Control_P;
        EventManager_Main.OnResolutionPanelButtonClicked += Resolution_P;
        EventManager_Main.OnAudioPanelButtonClicked += Audio_P;
        EventManager_Main.OnOptionConfirmButtonClicked += Option_confirm;
    }

    void OnDisable()
    {
        Debug.Log("해제");
        EventManager_Main.OnFindGameClicked -= FindGame;
        EventManager_Main.OnCustomGameClicked -= CustomGame;
        EventManager_Main.OnSelectClicked -= SelectCharacter;
        EventManager_Main.OnOptionClicked -= Option;
        EventManager_Main.OnQuitClicked -= Quit;
        EventManager_Main.OnSoloModeClicked -= Solo;
        EventManager_Main.OnMultiModeClicked -= Multi;
        EventManager_Main.OnControlPanelButtonClicked -= Control_P;
        EventManager_Main.OnResolutionPanelButtonClicked -= Resolution_P;
        EventManager_Main.OnAudioPanelButtonClicked -= Audio_P;
        EventManager_Main.OnOptionConfirmButtonClicked += Option_confirm;
    }

    void FindGame()
    {
        Debug.Log("FindGame 버튼 누름");
        // 새 게임 찾기 로직 구현 필요
        if (GameModeSelect != null)
            {
        GameModeSelect.SetActive(true);
        Debug.Log("있음");
            }
        else 
        {
            Debug.Log("없음");
        }
        
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
        NetworkManager.Instance.CreateRoom();
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    void Option()
    {
        Debug.Log("Option 버튼 누름");
        OptionUI.SetActive(true);
    }

    void Quit()
    {
        Debug.Log("Quit 버튼 누름");
    }
    void Solo()
    {
        Debug.Log("솔로 모드 선택");
        NetworkManager.Instance.JoinRoom();
        SceneController.Instance.LoadScene("LobbyScene");
        if (GameModeSelect != null)
        {
        GameModeSelect.SetActive(false);
        }
    }
    void Multi(){
        Debug.Log("멀티 모드 선택");
        if(GameModeSelect != null)
        {
        GameModeSelect.SetActive(false);
        }
    }
    void Control_P(){
        Debug.Log("컨트롤");
        ControlPanel.SetActive(true);
        resolutionPanel.SetActive(false);
        AudioPanel.SetActive(false);
    }
    void Resolution_P(){
        Debug.Log("해상도");
        ControlPanel.SetActive(false);
        resolutionPanel.SetActive(true);
        AudioPanel.SetActive(false);
    }
    void Audio_P(){
        Debug.Log("오디오");
        ControlPanel.SetActive(false);
        resolutionPanel.SetActive(false);
        AudioPanel.SetActive(true);
    }
    void Option_confirm(){
        OptionUI.SetActive(false);
    }
}
