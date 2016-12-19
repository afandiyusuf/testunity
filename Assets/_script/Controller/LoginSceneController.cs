using UnityEngine;
//! login scene controller
public class LoginSceneController : MonoBehaviour {

    public GameObject LoginScreen; /*!<objek screen login*/
    public GameObject RegisterScreen;/*!<objek screen regster*/
    /**
     * memunculkan objek login screen
     * */
    public void ShowLoginScreen()
    {
        LoginScreen.SetActive(true);
    }
    /**
 * memunculkan objek register screen
 * */
    public void ShowRegisterScreen()
    {
        RegisterScreen.SetActive(true);
    }
    /**
 * menyembunyikan objek semua screen (logindan register)
 * */
    private void HideAllScreen()
    {
        LoginScreen.SetActive(false);
        RegisterScreen.SetActive(false);
    }
}
