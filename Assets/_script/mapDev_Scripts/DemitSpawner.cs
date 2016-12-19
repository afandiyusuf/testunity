using UnityEngine;
using System.Collections;
//! munculnya musuh
public class DemitSpawner : MonoBehaviour {
	public GameObject player; //!< objek karakter/player
	private PlayerMovementMk1 pc;
	public GameObject battScene; //!< canvas prebattle
	Vector3 currentpos;
	public FollowingCamera camShake;

	public bool encounter; //!< munculnya animasi prebattle
	public bool inDemitArea = false; //!< didalam area munculnya musuh
	public bool isMoving = false; //!< karakter bergerak
	public static DemitSpawner instance; //!<script munculnya musuh

	int m;
	int n;
	float time;
	void Awake () 
	{
		m = 1;	
	}

	void Start () {		
		DemitSpawner.instance = this;
		//Invoke("ChangeState", 1f);
		pc = player.gameObject.GetComponent<PlayerMovementMk1>();
		battScene.SetActive(false);
		n = Random.Range(5,10);
		print(n);
	}

	void Update () {
		
		if (player.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0 || player.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0 )
		//if (player.hm)
		{
			isMoving = true;
			//Debug.Log("is Moving");
		}
		else
			isMoving = false;
		//Debug.Log("is not Moving");

		if (inDemitArea)
		{	
			if (isMoving)
				time += Time.deltaTime * m;			
		}

		//Debug.Log(time);
		if(time >= n){						
			battScene.SetActive(true);
			encounter = true;
			pc.speedMultiplier = 0;
			m = 0;
			time = 0;
			Invoke("ChangeState", 1.5f);

			//Invoke("backTo", 3f);
		}

		if (encounter == true && GetComponent<AudioSource>().isPlaying == false)
		{
			Invoke("ShakeCamera", 0.5f);
			SoundManager.instance.LowerVolume();
			GetComponent<AudioSource>().Stop();	
			GetComponent<AudioSource>().Play();
		}
					
	}

    /** kabur dari musuh**/
	public void backTo()
	{
		//yield return new WaitForSeconds(3);
		isMoving = false;
		encounter = false;
		battScene.SetActive(false);
		time = 0;
		pc.speedMultiplier = 15;
		m = 1;
	}
    /** player di dalam area musuh**/
	public void PlayerCheck(bool isIt)
	{
		inDemitArea = isIt;
	}

	void ChangeState()
	{
		encounter = false;
		GetComponent<AudioSource>().Stop();
	}

	void ShakeCamera()
	{		
		camShake.ShakeCamera(0.1f, 0.7f);
	}

}
