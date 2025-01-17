using System.Collections;
using System.Collections.Generic;
using GameScene.Item;
using UnityEngine;
using UnityEngine.UI;

public class Raticle : MonoBehaviour
{
    [Header("References")]
    public global::Item HeldItem; // 현재 장착된 아이템 (Item 기반)

    [Header("Crosshair Settings")]
    public Image crosshairImage; // 조준점 UI 이미지
    public Sprite farmingSprite; // 파밍 상태 원형 스프라이트
    public Sprite aimingSprite; // 총 장착 상태 원형 스프라이트

    [Header("Crosshair Colors")]
    public Color farmingColor = new Color(1f, 1f, 1f, 0.3f); // 파밍 상태 색상 (연함)
    public Color aimingColor = new Color(1f, 1f, 1f, 1f); // 총 장착 상태 색상 (진함)

    [Header("Crosshair Sizes")]
    public float farmingSize = 25f; // 파밍 상태 크기
    public float aimingSize = 100f; // 총 장착 상태 크기
    public float movingSize = 150f; // 이동 중 크기

    [Header("Transition Settings")]
    public float transitionSpeed = 5f; // 크기 및 색상 전환 속도

    private bool isMoving = false; // 이동 여부
    private Vector3 lastPosition; // 이전 프레임 위치

    void Start()
    {
        // 초기 조준점 상태 설정
        SetCrosshairState(farmingSprite, farmingColor, farmingSize);
        lastPosition = transform.position;
    }

    void Update()
    {
        // 이동 여부 감지
        isMoving = (transform.position - lastPosition).sqrMagnitude > 0.01f;
        lastPosition = transform.position;

        // 현재 장착된 아이템이 총인지 확인
        bool isAiming = HeldItem is Gun;

        // 상태에 따라 조준점 업데이트
        if (isAiming)
        {
            if (isMoving)
                UpdateCrosshair(movingSize, aimingColor);
            else
                UpdateCrosshair(aimingSize, aimingColor);
        }
        else
        {
            UpdateCrosshair(farmingSize, farmingColor);
        }
    }

    public void EquipItem(global::Item item)
    {
        HeldItem = item; // 현재 장착된 아이템 설정
    }

    private void UpdateCrosshair(float targetSize, Color targetColor)
    {
        // 크기와 색상을 부드럽게 전환
        crosshairImage.rectTransform.sizeDelta = Vector2.Lerp(crosshairImage.rectTransform.sizeDelta, new Vector2(targetSize, targetSize), Time.deltaTime * transitionSpeed);
        crosshairImage.color = Color.Lerp(crosshairImage.color, targetColor, Time.deltaTime * transitionSpeed);
    }

    private void SetCrosshairState(Sprite sprite, Color color, float size)
    {
        // 초기 상태 설정
        crosshairImage.sprite = sprite;
        crosshairImage.color = color;
        crosshairImage.rectTransform.sizeDelta = new Vector2(size, size);
    }

}
