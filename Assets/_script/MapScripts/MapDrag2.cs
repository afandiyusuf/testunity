using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//! drag item 2
public class MapDrag2 : MonoBehaviour, IBeginDragHandler, IDragHandler{
	/// <summary>
	/// The RectTransform that we are able to drag around.
	/// if null: the transform this Component is attatched to is used.
	/// </summary>
	public RectTransform dragObject; /*!<objek yang bisa di drag*/

	/// <summary>
	/// The area in which we are able to move the dragObject around.
	/// if null: canvas is used
	/// </summary>
	public RectTransform dragArea; /*!<area drag*/

    private Vector2 originalLocalPointerPosition;
	private Vector3 originalPanelLocalPosition;

	private RectTransform dragObjectInternal
	{
		get
		{
			if (dragObject == null)
				return (transform as RectTransform);
			else
				return dragObject;
		}
	}

	private RectTransform dragAreaInternal
	{
		get
		{
			if (dragArea == null)
			{
				RectTransform canvas = transform as RectTransform;
				while (canvas.parent != null && canvas.parent is RectTransform)
				{
					canvas = canvas.parent as RectTransform;
				}
				return canvas;
			}
			else
				return dragArea;
		}
	}
    /**
     * posisi saat awal di drag
     * */
	public void OnBeginDrag(PointerEventData data)
	{
		originalPanelLocalPosition = dragObjectInternal.localPosition;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(dragAreaInternal, data.position, data.pressEventCamera, out originalLocalPointerPosition);


	}
    /**
     * pada saat di drag
     * */
	public void OnDrag(PointerEventData data)
	{
		Vector2 localPointerPosition;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(dragAreaInternal, data.position, data.pressEventCamera, out localPointerPosition))
		{
			Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
			dragObjectInternal.localPosition = originalPanelLocalPosition + offsetToOriginal;
		}

		ClampToArea();
	}

	// Clamp panel to dragArea
	private void ClampToArea()
	{
		Vector3 pos = dragObjectInternal.localPosition;

		Vector3 minPosition = dragAreaInternal.rect.min - dragObjectInternal.rect.min;
		Vector3 maxPosition = dragAreaInternal.rect.max - dragObjectInternal.rect.max;

		pos.x = Mathf.Clamp(dragObjectInternal.localPosition.x, minPosition.x, maxPosition.x);
		pos.y = Mathf.Clamp(dragObjectInternal.localPosition.y, minPosition.y, maxPosition.y);

		dragObjectInternal.localPosition = new Vector3(Mathf.Clamp(dragObjectInternal.localPosition.x, -630, 630), Mathf.Clamp(dragObjectInternal.localPosition.y, -120,425), dragObjectInternal.localPosition.z);
	}
}
