using UnityEngine;
using UnityEngine.UI;
//! button controller
public class ButtonStateController : MonoBehaviour {
    public MasterButtonState masterButtonState; /*!<pemanggilan script MasterButtonState*/
    public GameObject selectedButton; /*!<button yang sudah di klik atau terpilih/selected*/
    public GameObject idleButton; /*!<buton yang tidak terpilih/terklik*/
    public int thisIndex; /*!<nomor index*/
    private Sprite activeSprite;
    private Sprite unactiveSprite;
    public Image Thumbnail; /*!<image thumbnail*/
    void Start()
    {
        masterButtonState = transform.parent.gameObject.GetComponent<MasterButtonState>();
    }
    /**
     * set sprite avatar sesuai thumbnail
     * */
    public void SetSprite(Sprite _thumbnail)
    {
        if (Thumbnail != null)
            Thumbnail.sprite = _thumbnail;
    }
    /**
     * idle button active dan semua button disable
     * */
    public void SetActive()
    {
        DisableAll();
        idleButton.SetActive(true);
    }
    /**
 * selected button active dan semua button disable
 * */
    public void SetUnactive()
    {
        DisableAll();
        selectedButton.SetActive(true);
    }
    /**
 * semua button disable
 * */
    void DisableAll()
    {
        selectedButton.SetActive(false);
        idleButton.SetActive(false);
    }
    /**
 * ketika klik button play sound klik dan script MasterButtonState active sesuai nomor index yang ada
 * */
    public void OnClickThis()
    {
        SoundManager.instance.PlayClickSound();
        masterButtonState.SelectButton(thisIndex);
    }
}
