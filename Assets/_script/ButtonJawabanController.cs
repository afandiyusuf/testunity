using UnityEngine;
using UnityEngine.UI;
//! button jawaban dari soal
public class ButtonJawabanController : MonoBehaviour,IButtonJawaban {
	public Text thisLabel; /*!<teks button*/
	
	private GameObject symbolPrefabs;
	private IMiniGameSoal Soal;

	private bool isTrueAnswer;

    /**
     * memunculkan jawaban yang benar atau salah
     * */
	public void initThisButton(IMiniGameSoal SoalReference,bool isTrueAnswer,string label)
	{
		this.Soal = SoalReference;
		this.isTrueAnswer = isTrueAnswer;
		thisLabel.text = label;
	}

    /**
     * mengirim jawaban yang akan dicek kebenarannya
     * */
	public void SendJawaban()
	{
		Soal.SendAnswer(isTrueAnswer);
	}
}
