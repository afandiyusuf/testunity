using UnityEngine;
using System.Collections;
//! testing controller player
public class playercontrollertest : MonoBehaviour {
	public float movespeed;/*!<speed karakter*/
    Animator moveani;
	Rigidbody2D rb;

	bool rightSide = true;
	bool frontSide;

	public ControllerX cx; /*!<pergerakan horizontal (sumbu X)*/
    public ControllerY cy;/*!<pergerakan vertical (sumbu Y)*/

    public VirtualController vc;/*!<contoller untuk pergerakan*/

    bool isUsingJoystick;

	// Use this for initialization
	void Start () {
		moveani = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}


	void FixedUpdate()
	{
		//Vector2 vectorMove = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) * movespeed;
		float hMove = Input.GetAxis("Horizontal");
		//float hMove = CrossPlatformInputManager.GetAxis("Horizontal");
		float vMove = Input.GetAxis("Vertical");
		//float vMove = CrossPlatformInputManager.GetAxis("Vertical");


		if(cx.InputDirection != Vector3.zero || cy.InputDirection != Vector3.zero)
		{
			hMove = cx.InputDirection.x;
			vMove = cy.InputDirection.z;
		}

		if(vc.InputDir != Vector3.zero)
		{		
			hMove = vc.InputDir.x;
			vMove = vc.InputDir.z;	


			//isUsingJoystick = true;
		}

		//Debug.Log (hMove + " " + vMove);
		/*if (isUsingJoystick)
		{
			hMove = vc.InputDir.x;
			vMove = vc.InputDir.z;
			//usingJoystick(vc.InputDir);
		}*/

		if ((hMove > 0 || hMove < 0))
		{
			moveani.SetBool("side",true);
			moveani.SetBool("front",false);
			moveani.SetBool("back",false);

			moveani.SetFloat("frontW", 0f);
			moveani.SetFloat("backW", 0f);

			rb.velocity = new Vector2(hMove*movespeed, rb.velocity.y);

			if(hMove > 0 &&!rightSide){
				Flip();	
				Debug.Log("kiri");
			}
			else if(hMove < 0 && rightSide){
				Flip();
				Debug.Log("kanan");
			}

		} 

		moveani.SetFloat("sideW", Mathf.Abs(hMove));

		if (vMove < 0 )
		{
			
			moveani.SetFloat("sideW", 0f);
			moveani.SetFloat("backW", 0f);

			moveani.SetBool("side",false);
			moveani.SetBool("front",true);
			moveani.SetBool("back",false);

			rb.velocity = new Vector2(rb.velocity.x, vMove*movespeed);
		}
		moveani.SetFloat("frontW", Mathf.Abs(vMove));

		if (vMove > 0 )
		{
			
			moveani.SetFloat("frontW", 0f);
			moveani.SetFloat("side", 0f);

			moveani.SetBool("side",false);
			moveani.SetBool("front",false);
			moveani.SetBool("back",true);

			rb.velocity = new Vector2(rb.velocity.x, vMove*movespeed);
		} 
		moveani.SetFloat("backW", Mathf.Abs(vMove));

	}

	/*void usingJoystick(Vector3 usevc)
	{
		float hMove = usevc.x;
		float vMove = usevc.y;

		if(hMove > 0 || hMove < 0)
		{
			rb.velocity = new Vector2(hMove*movespeed, rb.velocity.y);

			moveani.SetBool ("side", true);
			moveani.SetFloat("sideW", Mathf.Abs(hMove));

			if(hMove > 0 &&!rightSide)
				Flip();		
			else if(hMove < 0 && rightSide)
				Flip();			
		}
	}*/

	void Flip()
	{
		rightSide = !rightSide;
		Vector3 sScale = transform.localScale;
		sScale.x *= -1;
		transform.localScale = sScale;
	}

}
