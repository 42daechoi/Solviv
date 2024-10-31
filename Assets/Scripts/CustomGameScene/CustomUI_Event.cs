    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    using TMPro;
    using Photon.Pun;
    using Photon.Realtime;

    public class CustomUI_Event : MonoBehaviour
    {
        public GameObject CreateGameRoom;
        public TMP_InputField NumOfPeople;
        public int step = 1;
        public int minValue = 2;  // 최소 인원
        public int maxValue = 16;  // 최대 인원
        private int currentValue = 0; //방 인원
        private RoomInfo selectedRoom;

        void OnEnable(){
            EventManager_Custom.OnCreateButtonClicked += Create;
            EventManager_Custom.OnJoinButonClicked += Join;
            EventManager_Custom.OnSearchButtonClicked += Search;
            EventManager_Custom.OnPreviousButtonClicked += Previous;
            EventManager_Custom.OnIncreaseButtonClicked += Increase;
            EventManager_Custom.OnDecreaseButtonClicked += Decrease;
            EventManager_Custom.OnCancleButtonClicked += Cancle;
            EventManager_Custom.OnCreateComfirmButtonClicked += CreateComfirm;
            RoomList roomListComponent = FindObjectOfType<RoomList>();
                if (roomListComponent != null)
                {
                    roomListComponent.OnRoomSelected += OnRoomButtonClicked; // 방 선택 시 처리
                }
        }
        void OnDisable(){
            EventManager_Custom.OnCreateButtonClicked -= Create;
            EventManager_Custom.OnJoinButonClicked -= Join;
            EventManager_Custom.OnSearchButtonClicked -= Search;
            EventManager_Custom.OnPreviousButtonClicked -= Previous;
            EventManager_Custom.OnIncreaseButtonClicked -= Increase;
            EventManager_Custom.OnDecreaseButtonClicked -= Decrease;
            EventManager_Custom.OnCancleButtonClicked -= Cancle;
            EventManager_Custom.OnCreateComfirmButtonClicked -= CreateComfirm;
            RoomList roomListComponent = FindObjectOfType<RoomList>();
            if (roomListComponent != null)
            {
                roomListComponent.OnRoomSelected -= OnRoomButtonClicked; // 방 선택 시 처리
            }
        }
        void Start()
        {
            // Input Field 초기값 설정
            if (NumOfPeople.text != "")
            {
                currentValue = int.Parse(NumOfPeople.text);
            }
            else
            {
                currentValue = minValue;
                NumOfPeople.text = currentValue.ToString();
            }
        }
        void Create(){
            if (CreateGameRoom != null)
            {
                CreateGameRoom.SetActive(true);
            }
            Debug.Log("create버튼");
        }
        public void OnRoomButtonClicked(RoomInfo roomInfo)
        {
            selectedRoom = roomInfo;  // 선택된 방 정보 저장
            Debug.Log("선택된 방: " + selectedRoom.Name);
        }

        void Join(){
            if (selectedRoom != null)
            {
                PhotonNetwork.JoinRoom(selectedRoom.Name);  // 선택된 방 입장
                Debug.Log("방에 입장: " + selectedRoom.Name);
            }
            else
            {
                Debug.Log("선택된 방이 없습니다.");
            }
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
        void Increase(){
            currentValue += step;
            currentValue = Mathf.Clamp(currentValue, minValue, maxValue);
            NumOfPeople.text = currentValue.ToString();
        }
        void Decrease(){
            currentValue -= step;
            currentValue = Mathf.Clamp(currentValue, minValue, maxValue);
            NumOfPeople.text = currentValue.ToString();
        }
        public void OnInputFieldChanged()
        {
            if (NumOfPeople.text != "")
            {
                currentValue = int.Parse(NumOfPeople.text);
                currentValue = Mathf.Clamp(currentValue, minValue, maxValue);
                NumOfPeople.text = currentValue.ToString();
            }
        }
        public void Cancle(){
            if (CreateGameRoom != null)
                {
            CreateGameRoom.SetActive(false);
                }
        }
        public void CreateComfirm(){
            Debug.Log("방 생성 완료");
        }
    }
