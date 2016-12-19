using UnityEngine;
using System.Collections;
//! fungsi button dan joystick
public class UIbutton : MonoBehaviour {
	public GameObject joystickController;/*!<joystick*/
    public GameObject axisController;/*!<tombol arah pada jystick*/

    public bool onJs = false; /*!<aktif dan non-aktif*/
    /**
     * masuk ke scene "Demo_area1"
     * */
    public void LoadLevelArea1()
	{
		Application.LoadLevel("Demo_area1");
	}
    /**
     * masuk ke scene "Demo_area2"
     * */
	public void LoadLevelArea2()
	{
		Application.LoadLevel("Demo_area2");
	}
    /**
     * switch on-off
     * */
	public void ChangeController()
	{
		if(onJs == true)
		{
			onJs = false;
			Debug.Log("On is off");
		}else
		{
			onJs = true;
			Debug.Log("On is on");
		}

	}
    /**
     * fungsi quit app
     * */
	public void GoToQuit()
	{
		Application.Quit();
	}
	/*
	void Update()
	{
		if (onJs ==  true)
		{
			joystickController.SetActive(true);
			axisController.SetActive(false);
		}else
		{
			joystickController.SetActive(false);
			axisController.SetActive(true);
		}
	}*/
}
