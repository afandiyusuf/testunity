using UnityEngine;
using System.Collections;
//! pengaturan suara langkah kaki tiap player bergerak
public class FootSteps : MonoBehaviour {
	PlayerMovementMk1 pControl;

	void Start () {
		pControl = GetComponent<PlayerMovementMk1>();
	}
	

	void Update () {
		if (pControl.rbody.velocity.magnitude > 0)
			//Debug.Log("isMoving");
			GetComponent<AudioSource>().Play();
	}
}
