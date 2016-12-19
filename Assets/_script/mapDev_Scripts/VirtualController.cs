using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//! joystick virtual
public class VirtualController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler  
{
	private Image jscontainer;
	private Image jshandler;

	public Vector3 InputDir{set;get; }/*!<input arah pada joystick*/
    /**
     * di awal program akan mencari container virtual joystick.
     * arah input menjadi 0.
     * */
    public void Start()
	{
		jscontainer = GetComponent<Image>();
		jshandler = transform.GetChild(0).GetComponent<Image>();

		InputDir = Vector3.zero;
	}
    /**
     * ketika joystick digerakkan pada posisi kanan atau kiri dengan cara di drag maka arah joystick akan mengikuti
     * */
	public virtual void OnDrag(PointerEventData eda)
	{
		Vector2 pos = Vector2.zero;
		Vector2 rpos = Vector2.zero;
		if(RectTransformUtility.ScreenPointToLocalPointInRectangle
			(jscontainer.rectTransform,
				eda.position, eda.pressEventCamera, out pos))
		{
			rpos.x = jscontainer.rectTransform.sizeDelta.x;
			rpos.y = jscontainer.rectTransform.sizeDelta.y;

			pos.x = (pos.x / rpos.x);
			pos.y = (pos.y / rpos.y);

			float x = (jscontainer.rectTransform.pivot.x == 1) ? pos.x * 2+1 : pos.x * 2-1;
			float y = (jscontainer.rectTransform.pivot.y == 1) ? pos.y * 2+1 : pos.y * 2-1;

			InputDir = new Vector3 (x,0,y);

			InputDir = (InputDir.magnitude > 1) ? InputDir.normalized : InputDir;

			jshandler.rectTransform.anchoredPosition = 
				new Vector3(InputDir.x * (jscontainer.rectTransform.sizeDelta.x/3)
					,InputDir.z * (jscontainer.rectTransform.sizeDelta.y/3));

			//Debug.Log(InputDir);
		}
	}
    /**
     * arah joystick kebawah
     * */
	public virtual void OnPointerDown(PointerEventData eda)
	{
		OnDrag(eda);
	}
    /**
     * arah joystick keatas
     * */
	public virtual void OnPointerUp(PointerEventData eda)
	{
		InputDir = Vector3.zero;
		jshandler.rectTransform.anchoredPosition = Vector3.zero;
	}
}
