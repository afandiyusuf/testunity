using UnityEngine;
using System.Collections;
//! pengaturan layer untuk karakter.
public class LayerOrderScript : MonoBehaviour {
    public bool isStatic = true;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 10f) * -1;
    }

	void Update () {
        if (isStatic)
            return;

		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 10f) * -1;
	}
}
