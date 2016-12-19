using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//! warning teks
public class WarningController : MonoBehaviour {
	
    public GameObject WarningScreen;
    public Text Description_text; /*!<teks deskripsi warning*/
    public Text Title_text;/*!<title/judul dari teks warning*/


    void Start()
    {
        this.gameObject.SetActive(false);
    }

    /**
 * Fungsi untuk menampilkan warning panel.
 * description : deskripsi yang akan muncul di panel.
 * title : title yang akan muncul di warning panel.
*/
    public void Show(string description,string title)
    {
        WarningScreen.SetActive(true);
        Description_text.text = description;
        Title_text.text = title;
    }

    /**
     *  function called by button.
     *  hiden warning canvas.
    */
    public void HideThis()
    {
        //SoundManager.instance.PlayClickSound();
        Description_text.text = "";
        WarningScreen.SetActive(false);
    }

	
}
