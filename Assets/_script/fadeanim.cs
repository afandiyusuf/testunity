using UnityEngine;
using System.Collections;
//! animasi fade in ketika teleport
public class fadeanim : MonoBehaviour {
	Animator fadeani;

	void Start () 
	{
		fadeani = GetComponent<Animator>();	
	}
    /** animasi dijalankan / diaktifkan**/
	public void playanim(bool isIn)
	{
		fadeani.SetBool("inTransition", isIn);
		StartCoroutine ("notIn");
	}

	IEnumerator notIn()
	{
		yield return new WaitForSeconds(0.05f);
		fadeani.SetBool("inTransition", false);
	}
}
