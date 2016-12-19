using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
//!Mat tipe 1 adalah: soal gambar jawaban pilihan angka
public class MatTipe2 : MonoBehaviour,IMiniGameSoal {

    [System.Serializable]
    /*!<menentukan stage(jenis soal). 2=GenerateSoalGambar1(1,6). 4=GenerateSoalGambar1(6,10).*/
    public enum Stage
    {
        stage_2 = 2,
        stage_4 = 4
    }
    public Stage thisStage = Stage.stage_2; /*!<menentukan soal ini masuk kedalam stage berapa*/
    public Text SoalTop; /*!<teks tempat soal*/

    public GameObject[] AllButtonJawaban; /*!<semua button jawaban*/

    private List<GameObject> GarbageGO =new List<GameObject>();

    private CoreQuizController coreQuizManager;

    void InitSoal()
    {
       switch (thisStage)
       {
            case Stage.stage_2:
                GenerateSoalGambar1(1,6);
                break;
            case Stage.stage_4:
                GenerateSoalGambar1(6,10);
                break;
           default:
                Debug.Log("Default");
                break;
       }

    }
    /**
* membuat soal.
* jawaban benar 1 dan sisanya salah.
* generate button pilihan jawaban.
* */
    public void GenerateSoalGambar1(int minVal,int maxVal)
    {   
        /* Kelas_1 ada 2 variable
        /* 1 Soal 1-5 (bentuk gambar)
        /* 3 pilihan jawaban 1- 5 (angka)
        /*/
        int Jawaban = RecursiveRandom(new int[1]{TempStatic.lastSoal},minVal,maxVal);
        TempStatic.lastSoal = Jawaban;
        int delta = 1;
        int TrueChoice = Random.Range(0,3);
        SoalTop.text = Jawaban.ToString();

        int falseRandom1 = RecursiveRandom(new int[2]{0,Jawaban},minVal,maxVal);
       
        //generate button pilihan jawaban
        for(int i=0;i<3;i++)
        {
            if(i == TrueChoice)
            {
                AllButtonJawaban[i].GetComponent<IButtonJawaban>().initThisButton(this,true,Jawaban.ToString());
            }else{
                AllButtonJawaban[i].GetComponent<IButtonJawaban>().initThisButton(this,false,falseRandom1.ToString());
                falseRandom1 = RecursiveRandom(new int[3]{0,Jawaban,falseRandom1},minVal,maxVal);
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
