using UnityEngine;
using System.Collections;
//! beta controler untuk player
public class playerbetacontroller : MonoBehaviour {


	public float speed = 20f;/*!<speed karakter*/
    public Camera mainCam;/*!<camera yang mengikuti player*/
    bool rightSide = true;
	bool frontSide =true;
	Rigidbody2D rb;

	public GameObject gobutton;/*!<objek button menuju situs lain*/

    bool withNPC;

	//AREA1
	//------------------------------------//
	private Transform s1startPos;
	private Transform s1backPos;

	private Transform s2startPos;
	private Transform s2backPos;
	private Transform s2p1startPos;
	private Transform s2p1backPos;

	private Transform s3startPos;
	private Transform s3backPos;
	private Transform s3p1startPos;
	private Transform s3p1backPos;
	private Transform s3p2startPos;

	private Transform s4startPos;
	private Transform s4backPos;
	private Transform s4p1startPos;
	private Transform s4p1backPos;
	private Transform s4p2startPos;

	private Transform s5startPos;
	private Transform s5backPos;
	private Transform s5p1startPos;
	private Transform s5p1backPos;
	private Transform s5p2backPos1;
	private Transform s5p2backPos2;
	private Transform s5p2startPos1;
	private Transform s5p2startPos2;
	//-------------------------------------//
	//AREA2

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		withNPC = false;
		gobutton.gameObject.SetActive(false);

		//AREA1
		//==================================================================================//
		s1startPos = GameObject.Find("site1startpos").GetComponent<Transform>();
		s1backPos = GameObject.Find("fromsite1").GetComponent<Transform>();

		s2startPos = GameObject.Find("site2startpos").GetComponent<Transform>();
		s2backPos = GameObject.Find("fromsite2").GetComponent<Transform>();
		s2p1startPos = GameObject.Find("site2portalstart").GetComponent<Transform>();
		s2p1backPos = GameObject.Find("fromsite2portal").GetComponent<Transform>();

		s3startPos = GameObject.Find("site3startpos").GetComponent<Transform>();
		s3backPos = GameObject.Find("fromsite3").GetComponent<Transform>();
		s3p1startPos = GameObject.Find("site3aportalstart").GetComponent<Transform>();
		s3p1backPos = GameObject.Find("fromsite3bportal").GetComponent<Transform>();
		s3p2startPos = GameObject.Find("site3bportalstart").GetComponent<Transform>();

		s4startPos = GameObject.Find("site4startpos").GetComponent<Transform>();
		s4backPos = GameObject.Find("fromsite4").GetComponent<Transform>();
		s4p1startPos = GameObject.Find("site4astartpos").GetComponent<Transform>();
		s4p1backPos = GameObject.Find("fromsite4aportal").GetComponent<Transform>();
		s4p2startPos = GameObject.Find("site4bstartpos").GetComponent<Transform>();

		s5startPos = GameObject.Find("site5startpos").GetComponent<Transform>();
		s5backPos = GameObject.Find("fromsite5").GetComponent<Transform>();
		s5p1startPos = GameObject.Find("site5astartpos").GetComponent<Transform>();
		s5p1backPos = GameObject.Find("fromsite5aportal").GetComponent<Transform>();
		s5p2startPos1 = GameObject.Find("site5bstartpos1").GetComponent<Transform>();
		s5p2startPos2 = GameObject.Find("site5bstartpos2").GetComponent<Transform>();
		s5p2backPos1 = GameObject.Find("fromsite5bportal1").GetComponent<Transform>();
		s5p2backPos2 = GameObject.Find("fromsite5bportal2").GetComponent<Transform>();
		//==================================================================================//
	}

	void FixedUpdate () 
	{
		float sMove = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(sMove*speed, rb.velocity.y);

		if(sMove > 0 &&!rightSide)
			Flip();
		else if(sMove < 0 && rightSide)
			Flip();
	}
	void Flip()
	{
		rightSide = !rightSide;
		Vector3 sScale = transform.localScale;
		sScale.x *= -1;
		transform.localScale = sScale;
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			rb.velocity = new Vector2(rb.velocity.x, speed-5);
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			rb.velocity = new Vector2(rb.velocity.x, -speed-5);
		}
	}

	void OnCollisionEnter2D (Collision2D warp)
	{
		//AREA1
		//**********************************************************************************//
		//SITUS1
		if(warp.gameObject.name == "portals1" )
		{
			Vector3 s1Spos = s1startPos.position;
			gameObject.transform.position = s1Spos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(120,0,-57),1.5f);
		}else if(warp.gameObject.name == "site1exit")
		{
			Vector3 s1Bpos = s1backPos.position;
			gameObject.transform.position = s1Bpos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(0,0,-57),1.5f);
		}

		//SITUS2
		else if(warp.gameObject.name == "portals2")
		{
			Vector3 s2Spos = s2startPos.position;
			gameObject.transform.position = s2Spos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(-8, 90, -57),1.5f);
		}else if(warp.gameObject.name == "site2aportal")
		{
			Vector3 s2p1Spos = s2p1startPos.position;
			gameObject.transform.position = s2p1Spos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(-3, 165, -57),1.5f);
		}else if(warp.gameObject.name== "site2bportal")
		{
			Vector3 s2p1Bpos = s2p1backPos.position;
			gameObject.transform.position = s2p1Bpos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(-8, 90, -57),1.5f);
		}else if(warp.gameObject.name == "site2exit")
		{
			Vector3 s2Bpos = s2backPos.position;
			gameObject.transform.position = s2Bpos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(0,0,-57),1.5f);
		}

		//SITUS3
		else if(warp.gameObject.name == "portals3")
		{
			Vector3 s3Spos = s3startPos.position;
			gameObject.transform.position = s3Spos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(119, -80, -57),1.5f);
		}else if(warp.gameObject.name == "site3exit1")
		{
			Vector3 s3Bpos = s3backPos.position;
			gameObject.transform.position = s3Bpos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(0,0,-57),1.5f);
		}else if(warp.gameObject.name == "site3aportal")
		{
			Vector3 s3p2Spos = s3p2startPos.position;
			gameObject.transform.position = s3p2Spos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(-119, -80, -57),1.5f);
		}else if(warp.gameObject.name == "site3exit2")
		{
			Vector3 s3Spos = s3startPos.position;
			gameObject.transform.position = s3Spos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(119, -80, -57),1.5f);
		}else if(warp.gameObject.name == "site3bportal")
		{
			Vector3 s3p1Bpos = s3p1backPos.position;
			gameObject.transform.position = s3p1Bpos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(0, -80, -57), 1.5f);
		}

		//SITUS4
		else if(warp.gameObject.name == "portals4")
		{
			Vector3 s4Spos = s4startPos.position;
			gameObject.transform.position = s4Spos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(119,90,-57),1.5f);
		}else if(warp.gameObject.name == "site4exit")
		{
			Vector3 s4Bpos = s4backPos.position;
			gameObject.transform.position = s4Bpos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(0,0,-57),1.5f);
		}else if(warp.gameObject.name == "site4portal" || warp.gameObject.name == "site4bportal")
		{
			Vector3 s4p1Spos = s4p1startPos.position;
			gameObject.transform.position = s4p1Spos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(238,90,-57),1.5f);
		}else if(warp.gameObject.name == "site4aportal1")
		{
			Vector3 s4p2Spos = s4p2startPos.position;
			gameObject.transform.position = s4p2Spos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(238,150,-57),1.5f);
		}else if(warp.gameObject.name == "site4aportal2")
		{
			Vector3 s4p1Bpos = s4p1backPos.position;
			gameObject.transform.position = s4p1Bpos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(119,90,-57),1.5f);
		}

		//SITUS5
		else if(warp.gameObject.name == "portals5")
		{
			Vector3 s5Spos = s5startPos.position;
			gameObject.transform.position = s5Spos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(-352,0,-57),1.5f);
		}else if(warp.gameObject.name == "site5portal")
		{
			Vector3 s5p1Spos = s5p1startPos.position;
			gameObject.transform.position = s5p1Spos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(-235, 0,-57),1.5f);
		}else if(warp.gameObject.name == "site5aportal1")
		{
			Vector3 s5p1Bpos = s5p1backPos.position;
			gameObject.transform.position = s5p1Bpos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(-352,0,-57),1.5f);
		}else if(warp.gameObject.name == "site5aportal2")
		{
			Vector3 s5p2Spos1 = s5p2startPos1.position;
			gameObject.transform.position = s5p2Spos1;
			iTween.MoveTo(mainCam.gameObject, new Vector3(-119, 0 , -57),1.5f);
		}else if(warp.gameObject.name == "site5aportal3")
		{
			Vector3 s5p2Spos2 = s5p2startPos2.position;
			gameObject.transform.position = s5p2Spos2;
			iTween.MoveTo(mainCam.gameObject, new Vector3(-119,0,-57),1.5f);
		}else if(warp.gameObject.name == "site5bportal1")
		{
			Vector3 s5p2Bpos1 = s5p2backPos1.position;
			gameObject.transform.position = s5p2Bpos1;
			iTween.MoveTo(mainCam.gameObject, new Vector3(-235,0,-57),1.5f);
		}else if(warp.gameObject.name == "site5bportal2")
		{
			Vector3 s5p2Bpos2 = s5p2backPos2.position;
			gameObject.transform.position = s5p2Bpos2;
			iTween.MoveTo(mainCam.gameObject, new Vector3(-235,0,-57),1.5f);
		}else if(warp.gameObject.name == "site5exit")
		{
			Vector3 s5Bpos = s5backPos.position;
			gameObject.transform.position = s5Bpos;
			iTween.MoveTo(mainCam.gameObject, new Vector3(0,0,-57),1.5f);
		}
		//**********************************************************************************//

		//AREA2

	}

	void OnTriggerEnter2D(Collider2D wNPC)
	{
		if (wNPC.tag == "NPC")
		{
			withNPC = true;
			gobutton.gameObject.SetActive(true);
			Debug.Log("Hai");
		}
	}

	void OnTriggerExit2D(Collider2D wNPC)
	{
		if (wNPC.tag == "NPC")
		{
			withNPC = false;
			gobutton.gameObject.SetActive(false);
			Debug.Log("Bye");
		}
	}

}
