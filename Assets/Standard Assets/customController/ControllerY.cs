using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControllerY : MonoBehaviour, IPointerUpHandler, IPointerDownHandler 
{
	private Image yContainer;
	private Image yHandler;

	public Vector3 InputDirection {set;get;}
	private Vector3 yHandlerAnchor;
	void Start () 
	{
		yContainer = GetComponent<Image>();
		yHandler = transform.GetChild(0).GetComponent<Image>();
		yHandlerAnchor = yHandler.rectTransform.anchoredPosition;
	}

	public virtual void OnPointerDown(PointerEventData data)
	{
		Vector2 pos = Vector2.zero;
		Vector2 rpos= Vector2.zero;

		if(RectTransformUtility.ScreenPointToLocalPointInRectangle
			(yContainer.rectTransform, data.position, data.pressEventCamera, out pos))
		{
			rpos.y = yContainer.rectTransform.sizeDelta.y;
			pos.y = (pos.y / rpos.y)*3;

			float y = pos.y;

			InputDirection = new Vector3(0,0,y);
			InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

			yHandlerAnchor = new Vector3(0,0,InputDirection.y);


		}
	}
	public virtual void OnPointerUp(PointerEventData data)
	{
		InputDirection = Vector3.zero;
		yHandlerAnchor = Vector3.zero;

	
	}
}
