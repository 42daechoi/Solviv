using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionControl : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown; // 드롭다운 컴포넌트 연결

    private Resolution[] resolutions; // 사용 가능한 해상도 목록

    void Start()
    {
        // 사용 가능한 해상도 가져오기
        resolutions = Screen.resolutions;

        // 드롭다운 초기화
        resolutionDropdown.ClearOptions();

        // 해상도 옵션 문자열 리스트 생성
        var options = new System.Collections.Generic.List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(option);

            // 현재 해상도가 기본으로 설정되도록 인덱스 저장
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // 드롭다운에 옵션 추가 및 기본 해상도 선택
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // 드롭다운의 선택 변경 시 SetResolution 함수 호출
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    // 드롭다운 선택 시 호출될 해상도 변경 함수
    public void SetResolution(int resolutionIndex)
    {
        Resolution selectedResolution = resolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, FullScreenMode.Windowed);
    }
}
