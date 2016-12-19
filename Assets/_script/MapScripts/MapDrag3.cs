using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//! drag item 3
public class MapDrag3 : MonoBehaviour {
	public float speed = 0.1f; /*!<kecepatan*/
    public bool isInArea;/*!<didalam area atau tidak*/
    Vector3 lastPos;
	Vector3 startPos;

	void Start()
	{
		startPos = this.transform.position;
	}
    /**
     * mendeteksi di dalam area atau tidak
     * */
	public void DetectArea(bool inArea)
	{
		isInArea = inArea;
	}

	void Update() {
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);

			/*if (isInArea)
				transform.position = new Vector3 (Mathf.Clamp(transform.position.x, startPos.x+28f, startPos.x-28f), Mathf.Clamp(transform.position.y, startPos.x+0f, startPos.x-20f), transform.position.z);
			else
				transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);*/
		}	

		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0))
			lastPos = Input.mousePosition;
		
		if (Input.GetMouseButton(0))
		{
			//Vector3 delta = Camera.main.ScreenToViewportPoint(Input.mousePosition - lastPos);
			Vector3 delta = Input.mousePosition - lastPos;
			//Vector3 move = new Vector3(-delta.x * speed, -delta.y * speed, 0);
			transform.Translate(-delta.x * speed, -delta.y * speed, 0);
			//transform.Translate(move, Space.Self);
			lastPos = Input.mousePosition;

			/*if (isInArea)
				transform.position = new Vector3 (Mathf.Clamp(transform.position.x, startPos.x+28f, startPos.x-28f), Mathf.Clamp(transform.position.y, startPos.x+0f, startPos.x-20f), transform.position.z);
			else
				transform.Translate(-delta.x * speed, -delta.y * speed, 0);*/

		}
		#endif
	}


}
