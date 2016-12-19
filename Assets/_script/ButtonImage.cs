using UnityEngine;
using System.Collections;
using System;
//! gambar button
public class ButtonImage : MonoBehaviour,IButtonJawaban {

	public GameObject[] AllIcons;/*!<semua ibjek icon*/

    public int thisVal = 2;/*!<variable button jawaban*/

    private IMiniGameSoal Soal;
	private bool isTrueAnswer = false;
    /**
     * memunculkan semua button jawaban
     * */
	public void initThisButton(IMiniGameSoal SoalReference, bool isTrueAnswer=false, string label="")
    {
		this.Soal = SoalReference;
		int.TryParse(label,out thisVal);
		this.isTrueAnswer = isTrueAnswer;
		
        DisableAllButton();
        Debug.Log(this.thisVal);
		for(int i=0;i<thisVal;i++)
		{
			AllIcons[i].SetActive(true);
		}
    }
    /**
     * menyembunyikan semua button
     * */
	void DisableAllButton()
	{
		for(int i=0;i<AllIcons.Length;i++)
		{
			AllIcons[i].SetActive(false);
		}
	}
    /**
     * mengirim jawaban yang benar
     * */
	public void SendJawaban()
	{
		Soal.SendAnswer(isTrueAnswer);
	}
}
