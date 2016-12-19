using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
//!Mat tipe 3 adalah: soal 2 kelompok benda, jawaban 1 pilihan gambar
public class MatTipe3 : MonoBehaviour,IMiniGameSoal {

    public GameObject leftSoal; /*!<angka soal bagian kiri*/
    public GameObject rightSoal; /*!<angka soal bagian kanan*/

    public GameObject[] AllButtonJawaban; /*!<semua button jawaban*/

    private List<GameObject> GarbageGO =new List<GameObject>();

    private CoreQuizController coreQuizManager;

    void InitSoal()
    {
        GenerateSoal();
    }
    /**
 * membuat soal.
 * jawaban benar 1 dan sisanya salah.
 * generate button pilihan jawaban.
 * */
    public void GenerateSoal()
    {   
        /* Kelas_1 ada 2 variable
        /* 1 Soal 1-5 (bentuk gambar)
        /* 3 pilihan jawaban 1- 5 (angka)
        /*/
        int maxVal = 6;
        int leftNumber = Random.Range(1, maxVal);
       
        int maxvalNew = 10 - leftNumber;
        maxvalNew = (maxvalNew > 5) ? 5 : maxvalNew;
        int rightNumber = Random.Range(1, maxvalNew);
        int Jawaban = leftNumber + rightNumber;
        TempStatic.lastSoal = Jawaban;
        int delta = 1;
        int TrueChoice = Random.Range(0,3);
        int falseRandom1 = RecursiveRandom(new int[2]{0,Jawaban},1,maxVal);

        leftSoal.GetComponent<IButtonJawaban>().initThisButton(this, false, leftNumber.ToString());
        rightSoal.GetComponent<IButtonJawaban>().initThisButton(this, false, rightNumber.ToString());
        //generate button pilihan jawaban
        for(int i=0;i<3;i++)
        {
            if(i == TrueChoice)
            {
                AllButtonJawaban[i].GetComponent<IButtonJawaban>().initThisButton(this,true,Jawaban.ToString());
            }else{
                AllButtonJawaban[i].GetComponent<IButtonJawaban>().initThisButton(this,false,falseRandom1.ToString());
                falseRandom1 = RecursiveRandom(new int[3]{0,Jawaban,falseRandom1},1,maxVal);
            }
        }
    }
    /**
* soal yang sudah keluar tidak akan keluar lagi pada satu battle.
* */
    public void DestroyKelas()
    {
        if(GarbageGO == null)
            return;

        for(int i=0;i<GarbageGO.Count;i++)
        {
            Destroy(GarbageGO[i]);
        }
        GarbageGO.Clear();
        GarbageGO = null;
        System.GC.Collect();
    }

    private int RecursiveRandom(int[] excludesRandom,int minRandom,int maxRandom)
    {
        int ReturnVal = 0;
        ReturnVal = Random.Range(minRandom,maxRandom);

        for(int i=0;i<excludesRandom.Length;i++)
        {
            if(ReturnVal == excludesRandom[i])
            {
                return RecursiveRandom(excludesRandom,minRandom,maxRandom);
                break;
            }
        }
        return ReturnVal;
    }
    /**
* mengirim jawaban yang akan dicek kebenarannya.
* */
    public void SendAnswer(bool condition)
    {
        this.coreQuizManager.NextSoal((condition)?1:0);
    }
    /*Implement function */
    /**
* soal aktif dan memunculkan jenis soal yang ada.
* */
    public void LoadQuiz(int _nomorSoal, CoreQuizController coreQuizManager)
    {
        this.coreQuizManager = coreQuizManager;
        this.gameObject.SetActive(true);
        InitSoal();
    }
    public void DestroyQuiz()
    {

    }
    public void ResetQuiz()
    {

    }
    public void RestartQuiz()
    {

    }
    /**
* ketika selesai menjawab soal akan di hiden dan bila muncul lagi angka soal akan berubah.
* */
    public void HideQuiz()
    {
        DestroyKelas();
        this.gameObject.SetActive(false);
    }
    public void FinishAndNext()
    {

    }
    /* end of implement function */
}
