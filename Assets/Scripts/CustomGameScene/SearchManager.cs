using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchManager : MonoBehaviour
{
    public GameObject ContentHolder;  
    public GameObject[] Element;  
    public GameObject SearchBar;  
    public int totalElements;  
    
    
    public void UpdateElements()
    {
        totalElements = ContentHolder.transform.childCount;
        Element = new GameObject[totalElements];
        for (int i = 0; i < totalElements; i++)
        {
            Element[i] = ContentHolder.transform.GetChild(i).gameObject;
        }
    }

    // 검색 기능
    public void Search()
    {
        UpdateElements();  
        string SearchText = SearchBar.GetComponent<TMP_InputField>().text;  
        int searchTxtlength = SearchText.Length;  

        foreach (GameObject ele in Element)
        {
            if (ele != null)
            {
                
                TextMeshProUGUI roomNameText = ele.transform.Find("RoomName").GetComponent<TextMeshProUGUI>();

                if (roomNameText != null && roomNameText.text.Length >= searchTxtlength)
                {
                    
                    if (SearchText.ToLower() == roomNameText.text.Substring(0, searchTxtlength).ToLower())
                    {
                        ele.SetActive(true);  
                    }
                    else
                    {
                        ele.SetActive(false);  
                    }
                }
            }
        }
    }
}
