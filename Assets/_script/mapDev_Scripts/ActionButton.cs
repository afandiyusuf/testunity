using UnityEngine;
using System.Collections;
//! button teleport
public class ActionButton : MonoBehaviour {

	private GameObject player;
	private Camera mainCamera;
	public fadeanim fadeAnim;//!< animasi fadein
	public GameObject fadeline;//!< objek fadein
	Animator levelFade;

	public PlayerMovementMk1 pControl; //!< script player movement

	private Vector3 destination;
	private Vector3 cameraWarp;

	public bool isNPC; //!< detect NPC
	public bool isNeed; //!< detect portal

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		mainCamera = Camera.FindObjectOfType<Camera>();
		levelFade = fadeline.GetComponent<Animator>();
	}

    /** pengecekan NPC atau portal
     * jika NPC maka jalan kan function ChangeLevel
     * jika portal jalan kan function GoToSite
     * */
	public void buttonToogle()
	{
		if (isNPC)
			ChangeLevel();
		else
			GoToSite();
	}

    /** menentukan destinasi portal**/
	public void setDest(Transform t, Vector3 cWarp, bool need)
	{
		destination = t.position;
		cameraWarp = cWarp;
		isNeed = need;
	}
    /** perpindahan player ketika memasuki portal**/
	public void GoToSite()
	{
		if (isNeed)
		{
			player.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
		}else
			player.transform.localScale = new Vector3(1, 1, 1);;

        fadeAnim.playanim(true);
		player.gameObject.transform.position = cameraWarp; 
		Invoke("changePosition", 0.05f);
		iTween.MoveTo(mainCamera.gameObject, cameraWarp, 2.3f);
		Invoke("normalSpeed", 1f);
	}
    /** perpindahan karakter ketika berbicara dengan NPC**/
	public void ChangeLevel()
	{		
		fadeline.SetActive(true);
		levelFade.SetBool("inTransition2", true);
		player.gameObject.transform.position = cameraWarp;
		Invoke("changePosition", 0.05f);
		iTween.MoveTo(mainCamera.gameObject, cameraWarp, 1f);
		Invoke("normalSpeed", 1f);
		Invoke("DisableFade", 2f);
	}

	void changePosition()
	{
		player.gameObject.transform.position = destination;
	}

	void normalSpeed()
	{
		pControl.speedMultiplier = 15;
		Debug.Log("Control Online");
	}

	void DisableFade()
	{
		levelFade.SetBool("inTransition2", false);
		fadeline.SetActive(false);
	}


}	
