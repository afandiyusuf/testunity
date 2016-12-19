using UnityEngine;
using System.Collections;

//! Pergerakan dan kecepatan karakter
public class PlayerMovementMk1 : MonoBehaviour {
	public float speedMultiplier; //!< kecepatan karakter yang dimainkan player
	public AudioClip stepSound; //!< suara langkah kaki karakter ketika berjalan
	public GameObject stepTrail; //!< partikel asap saat berjalan
	public Rigidbody2D rbody; //!< rigidBody2D karakter
	Animator anim;

	void Start () {
		rbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void FixedUpdate () {		
		float hMove = ETCInput.GetAxis("Horizontal");
		float vMove = ETCInput.GetAxis("Vertical");

		Vector2 movementVector = new Vector2 (hMove, vMove);

		if (movementVector != Vector2.zero)
		{
			anim.SetBool ("isWalking", true);
			anim.SetFloat ("inputX", movementVector.x);
			anim.SetFloat ("inputY", movementVector.y);
		}else
		{
			anim.SetBool ("isWalking", false);
		}
		
		rbody.velocity = new Vector2((movementVector.x*speedMultiplier), (movementVector.y*speedMultiplier));

		if (hMove != 0 && GetComponent<AudioSource>().isPlaying == false || vMove != 0 && GetComponent<AudioSource>().isPlaying == false)
		//	Debug.Log("isMoving");
			GetComponent<AudioSource>().Play();
		else if (hMove == 0 && vMove == 0)
			GetComponent<AudioSource>().Stop();

		if (hMove != 0 || vMove != 0)
		{
			stepTrail.SetActive(true);
			stepTrail.GetComponent<ParticleSystem>().loop = true;
		}else
		{
			stepTrail.GetComponent<ParticleSystem>().loop = false;
			stepTrail.SetActive(false);
		}
	}

	
}
