using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raticle : MonoBehaviour
{
    private RectTransform reticle;
    
    public float restingSize;
    public float maxSize;
    public float speed;
    private float currentSize;
    private bool isMovingCache;
    
    private void Start()
    {
        reticle = GetComponent<RectTransform>();
        if (reticle == null)
        {
            Debug.LogError("RectTransform component is missing on this GameObject.");
        }
    }

    private void Update()
    {
        isMovingCache = Input.GetAxis("Horizontal") != 0 || 
                        Input.GetAxis("Vertical") != 0 || 
                        Input.GetAxis("Mouse X") != 0 || 
                        Input.GetAxis("Mouse Y") != 0;

        if (isMovingCache)
        {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }

        reticle.sizeDelta = new Vector2(currentSize, currentSize);
    }

    bool isMoving
    {
        get
        {
            return Input.GetAxis("Horizontal") != 0 || 
                   Input.GetAxis("Vertical") != 0 || 
                   Input.GetAxis("Mouse X") != 0 || 
                   Input.GetAxis("Mouse Y") != 0;
        }
    }

}
