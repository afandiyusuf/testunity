using UnityEngine;
using System.Collections;

public class BoxEditor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<BoxCollider2D>().size = new Vector2(100, 100);
	}
}
