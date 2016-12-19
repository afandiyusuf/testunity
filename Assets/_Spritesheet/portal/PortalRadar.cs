using UnityEngine;
using System.Collections;
//! pemanggilan sprite panah untuk portal
public class PortalRadar : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D rad)
	{
		if (rad.tag == "Player")
		{
			GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D rad)
	{
		if (rad.tag == "Player")
		{
			GetComponent<SpriteRenderer>().enabled = false;
		}
	}

}
