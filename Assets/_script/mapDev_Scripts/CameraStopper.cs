using UnityEngine;
using System.Collections;
//! camera didalam area
public class CameraStopper : MonoBehaviour {
	private Camera cam;
	public bool isInArea; /*!< didalam area*/
	//private Vector3 pos;

	void Start()
	{	
		cam = Camera.FindObjectOfType<Camera>();
		//pos = transform.position;
		//posX = transform.position.x;
		//posY = transform.position.y;
	}

	/*void Update () {
		if (isInArea == true){
			transform.position = new Vector3(Mathf.Clamp(transform.position.x,posX-39,posX+39),Mathf.Clamp(transform.position.y,posY-10,posY+10), transform.position.z);
			//Debug.Log("Out");
		}else{			
			//Debug.Log("In Area");
		}
	}*/

	void OnTriggerEnter2D(Collider2D playerCam)
	{
		if (playerCam.tag == "Player")
		{			
			isInArea = false;
			cam.GetComponent<FollowingCamera>().setPos(transform.position, isInArea);
			PlayerPrefs.SetFloat("xTemp", transform.position.x);
			PlayerPrefs.SetFloat("yTemp", transform.position.y);

			//Debug.Log("temp pos is " + PlayerPrefs.GetFloat("xTemp"));

		}
	}

	void OnTriggerExit2D(Collider2D playerCam)
	{
		if (playerCam.tag == "Player"){			
			isInArea = true;
			//if (cam.GetComponent<FollowingCamera>().isFollowing)
			cam.GetComponent<FollowingCamera>().setPos(transform.position, isInArea);
		}
	}
}
