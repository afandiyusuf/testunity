using UnityEngine;
using System.Collections;
//! cek camera ada didalam area atau tidak
public class areaDetector : MonoBehaviour {
	public bool areaDetected;/*!<cek area*/
	private Camera cam;

	void Start()
	{
		cam = Camera.FindObjectOfType<Camera>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "cameraStopper")
		{
			areaDetected = false;
			cam.GetComponent<MapDrag3>().DetectArea(areaDetected);
			//Debug.Log("well");
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "cameraStopper")
		{
			areaDetected = true;
			cam.GetComponent<MapDrag3>().DetectArea(areaDetected);
			//Debug.Log("great");
		}
	}
}
