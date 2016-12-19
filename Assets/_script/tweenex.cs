	using UnityEngine;
using System.Collections;
//! animasi perpindahan objek
public class tweenex : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveTo(this.gameObject,new Vector3(10,10,10),5);
	}
	
}
