using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseDrag : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
	public GameObject center;
	public RectTransform touchArea;
	public Image outerPad;
	public Image innerPad;

	private Vector2 joystickVector;

	public int direction; // 왼쪽 오른쪽 값 넣어서 moving 스크립트로 전달

	//private Coroutine runningCoroutine;

	public void OnDrag(PointerEventData eventData)
	{
		if(RectTransformUtility.ScreenPointToLocalPointInRectangle(touchArea, eventData.position, eventData.pressEventCamera, out Vector2 localPoint)) {
			localPoint.x = (localPoint.x / touchArea.sizeDelta.x);
			localPoint.y = (localPoint.y / touchArea.sizeDelta.y);

			joystickVector = new Vector2(localPoint.x, localPoint.y);

			Angle(joystickVector);

			joystickVector = (joystickVector.magnitude > 0.35f) ? joystickVector.normalized*0.35f : joystickVector;

			innerPad.rectTransform.anchoredPosition = new Vector2(joystickVector.x*(outerPad.rectTransform.sizeDelta.x), joystickVector.y*(outerPad.rectTransform.sizeDelta.y));
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		OnDrag(eventData);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		innerPad.rectTransform.anchoredPosition = Vector2.zero;
		direction = 0;
	}

	private void Angle(Vector3 currentJoystickVec)
	{
		Vector3 originJoystickVec = center.transform.right;

		float angle = Vector3.Angle(currentJoystickVec, originJoystickVec);
		//Debug.Log(angle);

		if (angle < 90) {
			direction = 1;
		}
		else {
			direction = -1;
		}
	}

	/*
	public void OnDrag(PointerEventData eventData)
	{
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
		transform.position = mousePosition;
	}*//*
	private Vector3 m_Offset; 
    private float m_ZCoord; 

    void OnMouseDown() 
    { 
        m_ZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z; 
        m_Offset = gameObject.transform.position - GetMouseWorldPosition(); 
    } 

    void OnMouseDrag() 
    { 
        transform.position = GetMouseWorldPosition() + m_Offset; 
    } 

    Vector3 GetMouseWorldPosition() 
    { 
        Vector3 mousePoint = Input.mousePosition; 
        mousePoint.z = m_ZCoord; 

        return Camera.main.ScreenToWorldPoint(mousePoint); 
    } */
}
