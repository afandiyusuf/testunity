using UnityEngine;
using UnityEngine.UI;
//! button jawaban
public class AnswerButton : MonoBehaviour {
    
    public Text label; /*!<teks jawaban dari soal*/
    private bool thisCondition;
    private MultipleChoiceManager multipleChoiceManager;

    void Start()
    {
        multipleChoiceManager = GameObject.FindObjectOfType<MultipleChoiceManager>();
    }

    /**
     * isi dari teks jawaban bisa benar, bisa salah
     * */
    public void InitThis(string label_text,bool condition)
    {
        label.text = label_text;
        thisCondition = condition;
    }

    /**
     * jika benar play sound win.
     * jika salah play sound lose.
     * cek kebenaran dari jawaban.
     * */
    public void SendAnswer()
    {
        if(thisCondition)
            SoundManager.instance.PlayWinSound();
        else
            SoundManager.instance.PlayLoseSound();
            
        multipleChoiceManager.SendAnswer(thisCondition);
        
    }
}
