using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControllerX : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
	private Image xContainer;
	private Image xHandler;

	public Vector3 InputDirection {set;get;}
	private Vector3 xHandlerAnchor;
	void Start () 
	{
		xContainer = GetComponent<Image>();
		xHandler = transform.GetChild(0).GetComponent<Image>();
		xHandlerAnchor = xHandler.rectTransform.anchoredPosition;
	}
	
	public virtual void OnPointerDown(PointerEventData data)
	{
		Vector2 pos = Vector2.zero;
		Vector2 rpos= Vector2.zero;

		if(RectTransformUtility.ScreenPointToLocalPointInRectangle
			(xContainer.rectTransform, data.position, data.pressEventCamera, out pos))
		{
			rpos.x = xContainer.rectTransform.sizeDelta.x;
			pos.x = (pos.x / rpos.x)*3;

			float x = pos.x;

			InputDirection = new Vector3(x,0,0);
			InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

			xHandlerAnchor = new Vector3(InputDirection.x,0,0);


		}
	}
	public virtual void OnPointerUp(PointerEventData data)
	{
		InputDirection = Vector3.zero;
		xHandlerAnchor = Vector3.zero;


	}

}
