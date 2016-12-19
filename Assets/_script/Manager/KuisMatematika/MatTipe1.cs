using UnityEngine;
using System.Collections.Generic;
//!Mat tipe 1 adalah: soal gambar jawaban pilihan angka

public class MatTipe1 : MonoBehaviour,IMiniGameSoal {

    [System.Serializable]
    /*!<menentukan stage(jenis soal). 1=GenerateSoalGambar1(1,6). 3=GenerateSoalGambar1(6,10).*/
    public enum Stage
    {
        stage_1 = 1,
        stage_3 = 3
    }
    public Stage thisStage = Stage.stage_1; /*!<menentukan soal ini masuk kedalam stage berapa*/
    public GameObject SimbolPrefabs; /*!<perfab simbol(gambar icon) soal*/
    public GameObject TopSoalContainer;/*!<container tempat soal*/

    public GameObject[] AllButtonJawaban;/*!<semua button jawaban dari soal*/

    private List<GameObject> GarbageGO =new List<GameObject>();

    private CoreQuizController coreQuizManager;

    void InitSoal()
    {
        switch (thisStage)
       {
            case Stage.stage_1:
                GenerateSoalGambar1(1,6);
                break;
            case Stage.stage_3:
                GenerateSoalGambar1(6,10);
                break;
           default:
                Debug.Log("Default");
                break;
       }

    }
    /**
     * Kelas_1 ada 2 variable.
        * 1 Soal 1-5 (bentuk gambar).
        * 3 pilihan jawaban 1- 5 (angka).
        * generate button pilihan jawaban.
     * */
    public void GenerateSoalGambar1(int minVal,int maxVal)
    {   		        
        int Jawaban = RecursiveRandom(new int[1]{TempStatic.lastSoal},minVal,maxVal);
        TempStatic.lastSoal = Jawaban;
        int delta = 1;
        int TrueChoice = Random.Range(0,3);
        GarbageGO = new List<GameObject>();
        for(int i=0;i<Jawaban;i++)
        {
            GameObject temp = (GameObject)Instantiate(SimbolPrefabs,Vector3.zero,Quaternion.identity);
            Transform tempTrans = temp.GetComponent<Transform>();
            tempTrans.SetParent(TopSoalContainer.GetComponent<Transform>());
			if (thisStage == Stage.stage_3)
			{
				tempTrans.localScale = new Vector3 (4,4,4);
				Debug.Log("size changed");
			}

            if(i<5){
                if(i==0)
                    tempTrans.localPosition = new Vector3(0,/*Random.Range(0,10)+*/20,0);
                else if(i%2 == 0)
					tempTrans.localPosition = new Vector3((i*60/2),/*Random.Range(0,10)+*/20,0);
                else
					tempTrans.localPosition = new Vector3(((i+1)*-60/2),/*Random.Range(0,10)+*/20,0);
            }else{
                int tempi = i-5;
                 if(tempi==0)
                    tempTrans.localPosition = new Vector3(0,/*Random.Range(0,10)*/-60,0);
                else if(tempi%2 == 0)
					tempTrans.localPosition = new Vector3((tempi*60/2),/*Random.Range(0,10)*/-60,0);
                else
					tempTrans.localPosition = new Vector3(((tempi+1)*-60/2),/*Random.Range(0,10)*/-60,0);
            }
            
            GarbageGO.Add(temp);
        }

        int falseRandom1 = RecursiveRandom(new int[2]{0,Jawaban},minVal,maxVal);
               
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
