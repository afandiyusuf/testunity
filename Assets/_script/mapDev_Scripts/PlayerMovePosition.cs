using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//! pergerakan karakter secara detail
public class PlayerMovePosition : MonoBehaviour {
	public Camera mainCam; //!< kamera yang mengikuti karakter
	PlayerMovementMk1 pControl;
	public GameObject fadeline; //!< ketika pindah ruangan akan muncul animasi fadein fadeout
	public DemitSpawner dSpawn; //!< script pemanggil musuh (demit)
	Animator linefade;

	private GameObject actButton;
    
    //bicara pada NPC
    private GameObject talkNpc;

    public bool inDemitArea = false; //!< mengecek karakter ada didalam area musuh atau tidak

	void Start()
	{			
		actButton = GameObject.FindGameObjectWithTag("actionButton");
        //bicara pada NPC
        talkNpc = GameObject.FindGameObjectWithTag("talk");
        //
        Invoke("DisablingButton",0.01f);
		pControl = GetComponent<PlayerMovementMk1>();
		linefade = fadeline.GetComponent<Animator>();
	}

	void Update()
	{
		DemitAreaCheck();
	}

	void OnCollisionEnter2D (Collision2D warp)
	{
		if(warp.gameObject.tag == "portal")
		{
			//Debug.Log("Enter portal");
			WarpControl wControl = warp.gameObject.GetComponent<WarpControl>();
			gameObject.transform.position = wControl.map.position;
			Vector3 cam = wControl.camStartPos;
			StartCoroutine ("temporaryPosition", wControl);
			iTween.MoveTo(mainCam.gameObject, cam, 1);
			if (cam != wControl.warpLoc.position){
				pControl.speedMultiplier = 0;
			}

			StartCoroutine ("delayControl", 0.1f);
		}

		if(warp.gameObject.tag == "LevelPortal")
		{
			WarpControl wControl = warp.gameObject.GetComponent<WarpControl>();
			fadeline.SetActive(true);
			linefade.SetBool("inTransition", true);
			Vector3 cam = wControl.camStartPos;
			gameObject.transform.position = wControl.map.position;
			StartCoroutine("disablefade");
			StartCoroutine("delaymove", wControl);
			StartCoroutine ("temporaryPosition", wControl);

			if (cam != wControl.warpLoc.position){
				pControl.speedMultiplier = 0;
			}

			StartCoroutine ("delayControl", 1.2f);
		}
	}

	void OnTriggerEnter2D(Collider2D act)
	{
		if (act.tag == "NPC")
		{
			Debug.Log("Hey, wanderer!");
			actButton.GetComponent<ActionButton>().isNPC = true;
            //bicara pada NPC
            actButton.gameObject.SetActive(false);
            talkNpc.gameObject.SetActive(true);				
        }

        if (act.tag == "sPortal")
		{
			Debug.Log("Are you goin', lad?");
			actButton.GetComponent<ActionButton>().isNPC = false;
			actButton.gameObject.SetActive(true);
            //bicara pada NPC
            //talkNpc.gameObject.SetActive(false);
        }

		if (act.tag == "DemitArea")
		{
			inDemitArea = true;
		}
		
	}

	void OnTriggerExit2D(Collider2D act)
	{
		if(act.tag == "NPC")
		{
			Debug.Log("Bye, then.");
			NPCControl npc= act.gameObject.GetComponent<NPCControl>();
            //bicara pada NPC
            talkNpc.gameObject.SetActive(false);
		}

		if (act.tag == "sPortal")
		{
			Debug.Log("Too bad...");
			SpecialWarpControl sWarp = act.gameObject.GetComponent<SpecialWarpControl>();
			actButton.gameObject.SetActive(false);
		}

		if (act.tag == "DemitArea")
		{
			inDemitArea = false;
		}
	} 

	IEnumerator delayControl(float moveDelay)
	{
		yield return new WaitForSeconds(moveDelay);
		pControl.speedMultiplier = 15;
		Debug.Log ("Control Online");
	}

	IEnumerator temporaryPosition(WarpControl wControl)
	{
		yield return new WaitForSeconds(0.05f);
		linefade.SetBool("inTransition", false);
		gameObject.transform.position = wControl.warpLoc.position;
	}

	IEnumerator delaymove(WarpControl wControl)
	{
		yield return new WaitForSeconds(0.04f);
		gameObject.transform.position = wControl.map.position;
		Vector3 cam = wControl.camStartPos;
		iTween.MoveTo(mainCam.gameObject, cam, 2);
	}

	IEnumerator disablefade()
	{
		yield return new WaitForSeconds(1.5f);
		fadeline.SetActive(false);
	}

	void DisablingButton()
	{
		actButton.gameObject.SetActive(false);
        //bicara pada NPC
        //talkNpc.gameObject.SetActive(false);
    }

    /** mengecek karakter ada didalam area musuh atau tidak
     * kalau iya maka script pemanggil musuh akan aktif
     * */
    public void DemitAreaCheck()
	{
		dSpawn.PlayerCheck(inDemitArea);
	}


}
