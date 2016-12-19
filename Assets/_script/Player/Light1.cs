using UnityEngine;
using System.Collections;
//! shading rambut
public class Light1 : MonoBehaviour {
	Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}


	void FixedUpdate () {
		//Input Method dengan keyboard
		float hMove = ETCInput.GetAxis("Horizontal");
		float vMove = ETCInput.GetAxis("Vertical");

		Vector2 movementVector = new Vector2 (hMove, vMove);

		if (movementVector != Vector2.zero)
		{
			anim.SetBool ("isMoving", true);
			anim.SetFloat ("x", movementVector.x);
			anim.SetFloat ("y", movementVector.y);
		}else
		{
			anim.SetBool ("isMoving", false);
		}
	}

	void Update()
	{		
		GetComponent<SpriteRenderer>().sortingOrder = (Mathf.RoundToInt(transform.position.y * 10f) * -1)+10;

	}
	//End of The Line

}
