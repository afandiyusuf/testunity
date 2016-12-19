using UnityEngine;

//!  script pemanggilan login dan register scene. 
/*!
  memanggil login diawak game dijalankan dan menon aktifkan login ketika masuk register scene begitu juga sebaliknya
*/
public class RegisterAndLoginController : MonoBehaviour {
    public ABaseSubScreen RegisterCanvas; /*!< canvas register acount player */
    public ABaseSubScreen LoginCanvas; /*!< canvas login player */

    void Start()
    {
        RegisterCanvas.HideScreen();
    }

    /**
     * ketika login aktif atau tampil dilayar maka canvas register tidak.
    **/
    public void ShowLoginCanvas()
    {
        //SoundManager.instance.PlayClickSound();
        LoginCanvas.ShowScreen();
        RegisterCanvas.HideScreen();
    }

    /**
     * ketika register aktif atau tampil dilayar maka canvas login tidak.
    **/
    public void ShowRegisterCanvas()
    {
        //SoundManager.instance.PlayClickSound();
        RegisterCanvas.ShowScreen();
        LoginCanvas.HideScreen();
    }

}
