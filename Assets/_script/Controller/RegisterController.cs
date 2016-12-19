using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//!  script register. 
/*!
  membuat akun baru dan dimasukkan langsung ke dalam database.
*/
public class RegisterController : ABaseSubScreen
{

    public Dropdown JenisKelamin; /*!< input jenis kelamin menggunakan dropdown yang tersedia (L/P) */
    public InputField Username; /*!< inputfield untuk memasukkan username */
    public InputField Alamat; /*!< inputfield untuk memasukkan alamat/email */
    public Dropdown Tanggal; /*!< dropdown untuk memilih tanggal lahir player */
    public Dropdown Bulan; /*!< dropdown untuk memilih bulan lahir player */
    public InputField Tahun; /*!< inputfield untuk memasukkan tahun lahir player */
    public InputField TempatLahir; /*!< inputfield untuk memasukkan tempat lahir player */
    public InputField Password; /*!< inputfield untuk memasukkan password */
    public InputField ConfirmPassword; /*!< inputfield untuk memasukkan konfirmasi ulang password */

    private bool isPasswordValidated = true;

    public Text WarningText; /*!< text peringatan bila player memasukkan password yang tidak sesuai dengan confirmPassword */
    public WarningController warningController; /*!< script pemanggilan peringatan salah password */

    private IDBModel s_dataModel;
    public RegisterAndLoginController registerAndLoginController; /*!< pemanggilan script contoller pemanggil login canvas dan register canvas */

    void Start()
    {
        s_dataModel = GameObject.FindGameObjectWithTag(HashTag.S_DATA_MODEL).GetComponent<IDBModel>();
        EmptyAllInputText();
    }

    void EmptyAllInputText()
    {
        WarningText.text = "";
        Username.text = "";
        Alamat.text = "";
        Tanggal.value = 0;
        Bulan.value = 0;
        Tahun.text = "";
        TempatLahir.text = "";
        Password.text = "";
        ConfirmPassword.text = "";
        JenisKelamin.value = 0;
    }

    /**
     * mengecek apakah password dan confirmPassword sudah sesuai, bila tidak player harus input ulang.
    **/
    public void ValidatePassword()
    {
        if (Password.text == ConfirmPassword.text)
        {
            Debug.Log("Password Sama");
            isPasswordValidated = true;
            WarningText.text = "";
        }
        else
        {
            Debug.Log("Password tidak sama");
            isPasswordValidated = false;
            WarningText.text = "Password yang anda ketikkan tidak sama";
        }
    }

    /**
     * bila password tidak valid dan username sudah ada yang menggunakan maka register digagalkan dan player harus mengisi ulang kolom yang salah.
    **/
    public void RegisterUser()
    {
        SoundManager.instance.PlayClickSound();
        if (!isPasswordValidated)
        {
            warningController.Show("Maaf password yang anda ketikkan tidak sama","Register Gagal");
            Debug.Log("Register Gagal");
        }
        else
        {
            if(Username.text == "" || Alamat.text == ""||TempatLahir.text == "" || Tahun.text == "")
            {
                warningController.Show("Maaf data yang anda masukkan tidak lengkap","Register Gagal");
            }else{
                if(s_dataModel.CheckUsername(Username.text) == 1)
                {
                    warningController.Show("Maaf username telah terpakai, gunakan username lain", "Username telah terpakai");
                    WarningText.text = "Username telah terpakai";
                    Username.text = "";
                }else{
                    string ttl = TempatLahir.text+", "+(Tanggal.value+1).ToString()+" - "+(Bulan.value+1).ToString()+" - "+Tahun.text.ToString();
                    string jk = (JenisKelamin.value == 0)?"L":"P";
                    s_dataModel.insert_user(Username.text,Password.text,ttl,jk,Alamat.text);
                    warningController.Show("Register sukses, silahkan masuk dengan akun dan password kamu","Terima kasih");
                    registerAndLoginController.ShowLoginCanvas();
                    EmptyAllInputText();
                }
            }
        }
    }
    /* Button Function */
    /* Interface's Promise */
    /**
     * memunculkan canvas register dan mengosongkan setiap inputField yang ada.
    **/
    public override void ShowScreen()
    {
        EmptyAllInputText();
        this.gameObject.SetActive(true);
    }

    /**
 * hiden canvas register.
**/
    public override void HideScreen()
    {
        this.gameObject.SetActive(false);
    }
    /* end of interface's Promise */

    /**
     * ketika register berhasil maka akan dilempar ke pembuatan avatar.
    **/
    public void GotoAvatarScreen()
    {
        SceneManager.LoadScene(HashTag.AVATAR_SCENE);
    }
    /* end of button's function */
}
