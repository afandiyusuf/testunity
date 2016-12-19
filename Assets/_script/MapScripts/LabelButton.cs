using UnityEngine;
using System.Collections;
//! button perpindahan scene
public class LabelButton : MonoBehaviour {
	public GameObject button;/*!<button untuk perpindahan scene*/
	//public Camera cam;
	public string id;/*!<nama scene untuk pindah*/
    public SceneMapManager sceneMan;/*!<memanggil script SceneMapManager*/
	//Vector3 moveCam;

	/*public void OnMouseUp()
	{		
		button.gameObject.SetActive(true);
		SendID();
		moveCam = new Vector3(transform.position.x, transform.position.y-8f, -10f);
		iTween.MoveTo(cam.gameObject, moveCam, 1);
	}*/
    /**
     * jika klik maka akan mengirim id.
     * */
	public void ClickLabel()
	{		
		button.gameObject.SetActive(true);
		SendID();
		//moveCam = new Vector3(transform.position.x, transform.position.y-8f, -10f);
		//iTween.MoveTo(cam.gameObject, moveCam, 1);
	}

	/*void FixedUpdate()
	{		
		if(cam.transform.position != moveCam)
		{			
			button.gameObject.SetActive(false);
		}

	}*/
    /**
     * setelah mengirim id akan dipindah ke scene yang dituju.
     * */
	public void SendID()
	{
		sceneMan.GetID(id);
	}


}
