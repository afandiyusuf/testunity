using UnityEngine;
using System.Collections;
//! beta controller 2
public class playersystembeta2 : MonoBehaviour {

	public Camera mainCam;/*!<main camera*/
    Rigidbody2D rb;

	//AREA2
	//-------------------------------------//
	private Transform a2startPos;

	private Transform a2s0startPos;
	private Transform a2s0Exit;
	//-------------------------------------//

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();

		//AREA2
		//==================================================================================//
		a2startPos = GameObject.Find("area2startpos").GetComponent<Transform>();

		a2s0startPos = GameObject.Find("a2site0startpos").GetComponent<Transform>();
		a2s0Exit = GameObject.Find("froma2site0").GetComponent<Transform>();

		//==================================================================================//
	}

	void OnCollisionEnter2D (Collision2D tele)
	{
		//SITUS1
		if (tele.gameObject.name == "portals0")
		{
			Debug.Log("Welcome!");
			Vector3 a2s0Spos = a2s0startPos.position;
			gameObject.transform.position = a2s0Spos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(0,80,-57),1.5f);
		}if(tele.gameObject.name == "a2site0exit")
		{
			Vector3 a2s0Bpos = a2s0Exit.position;
			gameObject.transform.position = a2s0Bpos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(0,0,-57),1.5f);
		}
		//**********************************************************************************//
	}

}
