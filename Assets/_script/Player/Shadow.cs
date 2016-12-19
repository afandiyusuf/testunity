using UnityEngine;
using System.Collections;
//! pengaturan bayangan pada karakter
public class Shadow : MonoBehaviour {

	void Update () {
		GetComponent<SpriteRenderer>().sortingOrder = (Mathf.RoundToInt(transform.position.y * 10f) * -1)-10;
	}
}
