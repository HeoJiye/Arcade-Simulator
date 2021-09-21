﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public static float deltaX, deltaY;

    public Image bgImg; // 조이스틱 배경
    public Image joystickImg; // 조이스틱
    private Vector3 inputVector;

    public virtual void OnDrag(PointerEventData ped) // 배경 이미지가 터치받으면 조이스틱이 터치받은 곳으로 이동
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 1, pos.y * 2, 0);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            // 조이스틱 이동
            joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3)
                                                                , inputVector.y * (bgImg.rectTransform.sizeDelta.y / 3));
        }
    }

    public virtual void OnPointerDown(PointerEventData ped) // 터치하고 있을 때
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped) // 터치 안할 때 원위치
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;

    }

    void Update() {
        deltaX = inputVector.x*200f;
        deltaY = inputVector.y*200f;
    }

}
