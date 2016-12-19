using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! pergerakan NPC
public class NPCRandomMovement : MonoBehaviour {
	public float movespeed; //!< kecepatan berjalan

	public Collider2D walkZone; //!< zona pergerakan (dari sudut ke sudut colider)
	private Vector2 minPoint;
	private Vector2 maxPoint;

	public bool isWalking; //!< NPC bergerak
	public bool isNear; //!< NPC diam (tidak bergerak)

	public bool moveHorizontal; //!< NPC hanya bergerak horizontal
	public bool moveVertical; //!< NPC hanya bergerak vertikal

	public float walkTime; //!< waktu yang diperlukan NPC berjalan dari point A ke point B
	private float walkCounter;
	public float waitTime; //!< waktu yang diperlukan NPC ketika diam (tidak bergarak)
	private float waitCounter;

	private int walkDirection;
	private bool hasZone;

	Rigidbody2D rb;
	Animator anim;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		walkCounter = walkTime;
		waitCounter = waitTime;
		if (walkZone != null)
		{
			minPoint = walkZone.bounds.min;
			maxPoint = walkZone.bounds.max;	
			hasZone = true;
		}

	}

	void Update()
	{
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 10f) * -1;

		if (isWalking)
		{
			walkCounter -= Time.deltaTime;

			if (walkCounter < 0)
			{
				isWalking = false;
				waitCounter = waitTime;
			}

			if (moveHorizontal && moveVertical)
			{
				switch (walkDirection)
				{
				case 0: 
					rb.velocity = new Vector2(0, movespeed);
					anim.SetFloat ("inputY", 1);
					anim.SetBool ("isWalking", true);
					if (hasZone && transform.position.y > maxPoint.y)
					{
						isWalking = false;
						waitCounter = waitTime;
					}
					break;
				case 1: 
					rb.velocity = new Vector2(movespeed, 0);
					anim.SetFloat ("inputX", 1);
					anim.SetBool ("isWalking", true);
					if (hasZone && transform.position.x > maxPoint.x)
					{
						isWalking = false;
						waitCounter = waitTime;
					}
					break;
				case 2: 
					rb.velocity = new Vector2(0, -movespeed);
					anim.SetFloat ("inputY", -1);
					anim.SetBool ("isWalking", true);
					if (hasZone && transform.position.y < minPoint.y)
					{
						isWalking = false;
						waitCounter = waitTime;
					}
					break;
				case 3: 
					rb.velocity = new Vector2(-movespeed, 0);
					anim.SetFloat ("inputX", -1);
					anim.SetBool ("isWalking", true);
					if (hasZone && transform.position.x > minPoint.x)
					{
						isWalking = false;
						waitCounter = waitTime;
					}
					break;
				}
			}

			else if(moveHorizontal && !moveVertical)
			{
				switch (walkDirection)
				{
				case 0:
					rb.velocity = new Vector2(movespeed, 0);
					anim.SetFloat ("inputX", 1);
					anim.SetBool ("isWalking", true);
					if (hasZone && transform.position.x > maxPoint.x)
					{
						isWalking = false;
						waitCounter = waitTime;
					}
					break;
				case 1:
					rb.velocity = new Vector2(-movespeed, 0);
					anim.SetFloat ("inputX", -1);
					anim.SetBool ("isWalking", true);
					if (hasZone && transform.position.x < minPoint.x)
					{
						isWalking = false;
						waitCounter = waitTime;
					}
					break;
				}
			}

			else if(!moveHorizontal && moveVertical)
			{
				switch (walkDirection)
				{
				case 0: 
					rb.velocity = new Vector2(0, movespeed);
					anim.SetFloat ("inputY", 1);
					anim.SetBool ("isWalking", true);
					if (hasZone && transform.position.y > maxPoint.y)
					{
						isWalking = false;
						waitCounter = waitTime;
					}
					break;
				case 1: 
					rb.velocity = new Vector2(0, -movespeed);
					anim.SetFloat ("inputY", -1);
					anim.SetBool ("isWalking", true);
					if (hasZone && transform.position.y < minPoint.y)
					{
						isWalking = false;
						waitCounter = waitTime;
					}
					break;
				}
			}
		}

		else
		{
			waitCounter -= Time.deltaTime;
			rb.velocity = Vector2.zero;

			anim.SetFloat ("inputX", 0);
			anim.SetFloat ("inputY", 0);
			anim.SetBool ("isWalking", false);
			if (waitCounter < 0)
			{
				MovementDirection();
			}
		}
	}
	
    /** arah mana saja NPC bergerak
     * bila moveHorizontal aktif maka hanya bergerak horizontal
     * bila moveVertical aktif maka hanya bergerak vertikal
     * bila keduanya aktif NPC akan bergerak random
     * */
	public void MovementDirection()
	{
		if (moveHorizontal && moveVertical)
		{		
			walkDirection = Random.Range(0,4);	
			isWalking = true;
			walkCounter = walkTime;
		}else
		{
			walkDirection = Random.Range(0,2);
			isWalking = true;
			walkCounter = walkTime;
		}
	}

	/*void OnTriggerEnter2D(Collider2D rad)
	{
		
	}*/
}
