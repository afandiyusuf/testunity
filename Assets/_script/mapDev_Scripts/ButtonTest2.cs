using UnityEngine;
using System.Collections;
//! button teleport masuk situs selanjutnya
public class ButtonTest2 : MonoBehaviour {
	private GameObject player;
	private Camera mainCam;

	public int status = 99; /*!<menentukan tujuan*/

	public SpecialWarpControl swc; /*!<memanggil script SpecialWarpControl*/

	//private GameObject[] specialPortal;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		mainCam = Camera.FindObjectOfType<Camera>();
		//specialPortal = GameObject.FindGameObjectsWithTag("sPortal");
	}
    /**
     * fungsi menentukan tujuan lewat integer status.
     * */
	public void buttonToggle()
	{
		if(this.status == 0)
		{
			Debug.Log("NPCa");
		}else if(this.status == 1)
		{
			Debug.Log("NPCb");
		}
			
			
	}
	/**
     * teleport menuju situs selanjutnya.
     * perpindahan posisi karakter dan kamera
     * */
	public void GotoSite1()
	{
		
		Vector3 cam = swc.cameraWarp;
		player.gameObject.transform.position = swc.specialWarpLoc.position; 
		iTween.MoveTo(mainCam.gameObject, cam, 1);
		//if()
		//{
			//SpecialWarpControl swc = gameObject.GetComponent<SpecialWarpControl>();
			//Debug.Log(swc);
			//player.transform.position =
		//}
		//player.transform.position =  sWarp.specialWarpLoc.position;

	}

	public void GotoSite2()
	{
		//Debug.Log(specialPortal.GetValue(2));
		//player.transform.position = sWarp.specialWarpLoc.position;
	}
}
