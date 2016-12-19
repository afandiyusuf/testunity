using UnityEngine;
using System.Collections;
//! pemanggilan partikel asap ketika pemain bergerak
public class ParticlesLayerOrder : MonoBehaviour {

	void Update () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = (Mathf.RoundToInt(transform.position.y * 10f) * -1)-100;
	}
}
