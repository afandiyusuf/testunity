using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

//!  script login. 
/*!
  login kedalam game berdasarkan data yang ada di database.
*/
public class LoginController : ABaseSubScreen
{

    private IDBModel sDataModel;
    private InputField[] AllInputComponent;

    public InputField username; /*!<input field untuk input user name player*/
    public InputField password; /*!<input field untuk input password player*/
    public WarningController warningController;  /*!<memanggil script warningController*/

    void Start()
    {
     
        AllInputComponent = new InputField[] { username, password };

        sDataModel = GameObject.FindGameObjectWithTag(HashTag.S_DATA_MODEL).GetComponent<IDBModel>();
    }

    void EmptyAllInputText()
    {
        for (int i = 0; i < AllInputComponent.Length; i++)
        {
            AllInputComponent[i].text = "";
        }
    }

    /**
     * input username dan password sesuai yang ada di databese.
     * bila benar masuk kedalam game bila salah muncul popup warning.
     * */
    public void Login()
    {
        int loginStatus = 0;
        loginStatus = sDataModel.Login(username.text, password.text);
        if (loginStatus == 1)
        {
            #if UNITY_EDITOR
            Debug.Log("Success");
            #endif

            SceneManager.LoadScene(HashTag.AVATAR_SCENE);
        }
        else
        {
            #if UNITY_EDITOR
            Debug.Log("Error");
            #endif
            
            warningController.Show("Maaf user/password salah","WARNING");
        }
        EmptyAllInputText();
    }


    //interface's promise
    /**
     * kosongkan semua inputfiled yang ada.
     * munculkan canvas login.
     * */
    public override void ShowScreen()
    {
        EmptyAllInputText();
        this.gameObject.SetActive(true);
    }

    /**
  * hiden canvas login.
  * */
    public override void HideScreen()
    {
        this.gameObject.SetActive(false);
    }
}
