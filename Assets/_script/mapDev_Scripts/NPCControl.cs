using UnityEngine;
using System.Collections;
//! teleport lewat NPC
public class NPCControl : MonoBehaviour {
	
	private ActionButton actButton;


	public Transform NPCWarpLoc;/*!<teleport ke lokasi*/
	public Transform map;/*!<map tempat teleport*/
    public Transform camerastart;/*!<posisi awal kamera*/

    public Vector3 cameraWarp;/*!<posisi kamera ketika teleport*/
    private int cameraZ = -40;/*!<posisi z dari kamera*/

    public bool needEnlarge;/*!<boolean untuk menentukan posisi*/

    void Start()
	{
		cameraWarp = new Vector3(NPCWarpLoc.position.x, NPCWarpLoc.position.y, cameraZ);
		actButton = GameObject.FindGameObjectWithTag("actionButton").GetComponent<ActionButton>();

	}

	void OnTriggerEnter2D(Collider2D player)
	{
		if(player.gameObject.tag == "Player")
		{			
			actButton.setDest(NPCWarpLoc, cameraWarp, needEnlarge);
		}
	}

	void OnTriggerExit2D(Collider2D player)
	{
		if(player.gameObject.tag == "Player")
		{
			//actButton.IDGot = "";
		}
	}


}
