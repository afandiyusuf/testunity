using UnityEngine;
using System.Collections;
//! button teleport lewat NPC
public class buttontest : MonoBehaviour {

	private Camera maincam;
	private GameObject player;


	public Transform NPCWarpLoc;/*!<lokasi teleport*/
	public Transform map;/*!<map tempat teleport*/

	public Vector3 cameraWarp;/*!<posisi kamera ketika teleport*/
	public int cameraZ = -57; /*!<posisi Z pada kamera*/
	//private Transform s3p1startPos;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		maincam = Camera.FindObjectOfType<Camera>();

		cameraWarp = new Vector3(map.position.x, map.position.y, cameraZ);
		//s3p1startPos = GameObject.Find("site3aportalstart").GetComponent<Transform>();
	}
    /**
     * fungsi teleport pada NPC
     * */
	public void NPCbutton()
	{		
			
			Vector3 cam = cameraWarp;
			player.transform.position = NPCWarpLoc.position;
			iTween.MoveTo(maincam.gameObject, cam,1.5f);
	}
	
}
