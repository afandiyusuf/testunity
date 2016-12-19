using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//! pengaturan button pembuatan avatar
public class MasterButtonState : MonoBehaviour {

    public ButtonStateController[] ButtonStateGroup; /*!< pemanggilan array pada script ButtonStateController*/
    public SpriteAvatarSelector[] AvatarController; /*!< pemanggilan array pada script SpriteAvatarSelector*/
    public Sprite[] Thumbnails; /*!<array thumbnails avatar*/
    public string title; /*!<judul scene*/


    public Text titleText; /*!<teks judul*/
    public int DefaultIndex; /*!<jumlah index*/

    void Start()
    {
        
        SetTitle();
        SelectButton(DefaultIndex);
        SetAllSpriteButton();
    }
    private void SetTitle()
    {
        if (titleText != null)
            titleText.text = title;
    }

    /**
     * pengaturan button sprite avatar
     * */
    public void SetAllSpriteButton()
    {
        if (Thumbnails == null)
            return;

        for (int i = 0; i < ButtonStateGroup.Length && i < Thumbnails.Length ; i++)
        {
            ButtonStateGroup[i].SetSprite(Thumbnails[i]);
        }

    }

    /**
 * ketika mengklik button
 * */
    public void SelectButton(int index)
    {
        
        int i;
        for (i = 0; i < ButtonStateGroup.Length; i++)
        {
            if (ButtonStateGroup[i].thisIndex == index)
            {
                ButtonStateGroup[i].SetUnactive();
            }
            else
            {
                ButtonStateGroup[i].SetActive();
            }
        }

        for (i = 0; i < AvatarController.Length; i++)
        {
            AvatarController[i].SelectByIndex(index);
        }
        
    }
    /**
 * play sound ketika button diklik
 * */
    public void PlaySound()
    {
        SoundManager.instance.PlayClickSound();
    }



}
