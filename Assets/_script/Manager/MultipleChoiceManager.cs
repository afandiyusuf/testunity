using UnityEngine;
using UnityEngine.UI;
//! pengaturan pilihan ganda
public class MultipleChoiceManager : MonoBehaviour,IMiniGameSoal {
    public AnswerButton[] AllAnswerButtons; /*!<kumpulan semua button jawaban yang ada*/
    public Text BigAnswers_text;/*!<teks jawaban*/
    public Text QuizLabel_text;/*!<teks label quiz*/
    //public TextProcessor textProcessor;
    private string initString;

    public float BigAnswers; /*!<jawaban yang dipilih*/
    public int BigAnswersDelta;/*!<random isi jawaban*/
    public float QuizHint;/*!<hint pada quiz*/
    public int QuizHintDelta;/*!<jumlah hint pada quiz*/
    public float TrueAnswers;/*!<jawaban yang benar*/
    public int TrueAnswersDelta;/*!<nilai jawaban yang benar*/
    private float TrueChoice;

    private CoreQuizController coreQuizController;

    public int kelas = 0; /*!<nilai integer dari kelas*/
    public  int nomorSoal; /*!<nomor soal*/
    public int indexBankSoal; /*!<index dari seluruh soal*/
    public int totalBankSoal; /*!<total dari seluruh soal*/
    public float minVal; /*!<minimal value random range*/
    public int minDelta; /*!<nilai dari minimal value*/
    public float maxVal; /*!<maximal value random range*/
    public int maxDelta; /*!<nilai dari maximal value*/
    public float delta; /*!<nilai acuan pembagian*/




    void Awake()
    {
        initString = QuizLabel_text.text;
    }

    void InitMultipleChoice()
    {
        totalBankSoal = 3;
        minDelta = Mathf.FloorToInt(minVal / delta);
        maxDelta = Mathf.FloorToInt(maxVal / delta);

        QuizHintDelta = Random.Range(minDelta*2, maxDelta*2);
        QuizHint = QuizHintDelta * delta;


        //Randomize Angka yang paling kanan (Jawaban yang gede)
        BigAnswersDelta = Random.Range(minDelta, QuizHintDelta);
        BigAnswers = BigAnswersDelta * delta;
        BigAnswers_text.text = BigAnswers.ToString();

        //Randomize Angka yang ada di soal
        
      

        QuizLabel_text.text = initString + QuizHint.ToString() + "?";

        //Calculate jawaban yang benar
        TrueAnswers = QuizHint - BigAnswers;
        TrueAnswersDelta = Mathf.FloorToInt(TrueAnswers/delta);
        //Randomize tombol mana yang benar
        TrueChoice = Random.Range(0, 3);
        int falseOneChoice = 0;
        for (int i = 0; i < AllAnswerButtons.Length; i++)
        {
            if(i == TrueChoice)
            {
                AllAnswerButtons[i].InitThis(TrueAnswers.ToString(),true);
            }
            else
            {
                int RandomDelta = RecursiveRandom(TrueAnswersDelta, falseOneChoice);
                AllAnswerButtons[i].InitThis((RandomDelta*delta).ToString(), false);
            }
            
        }

    }

    private int RecursiveRandom(int excludeNumber1,int excludeNumber2)
    {
        int ReturnVal;
        ReturnVal = Random.Range(minDelta, QuizHintDelta);

        if (ReturnVal != excludeNumber1 && ReturnVal != 0 && ReturnVal != excludeNumber2)
            return ReturnVal;
        else
            return RecursiveRandom(ReturnVal, excludeNumber2);
    }
    /**
     * mengirim jawaban (benar atau salah) untuk mendapatkan soal selanjutnya
     * */
    public void SendAnswer(bool condition)
    {
        //jika condition true yang dikirim 1
        //else yang dikirim 0
        coreQuizController.NextSoal((condition) ? 1 : 0);

#if UNITY_EDITOR
        Debug.Log(condition);
#endif
    }
    /*Implement function */
    /**
     * menentukan nomor soal
     * */
    public void LoadQuiz(int _nomorSoal, CoreQuizController coreQuizManager)
    {
        int temp = _nomorSoal - 1;
        this.nomorSoal = 1;//temp % textProcessor.processedText[kelas].soal.Length;
        
        this.gameObject.SetActive(true);
        this.coreQuizController = coreQuizManager;
        
        //float.TryParse(textProcessor.processedText[kelas].soal[nomorSoal].minVal,out minVal);
        minVal = 20;
        //float.TryParse(textProcessor.processedText[kelas].soal[nomorSoal].maxVal, out maxVal);
        maxVal = 100;
        delta = 1;
        //float.TryParse(textProcessor.processedText[kelas].soal[nomorSoal].delta, out delta);
        InitMultipleChoice();
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
     * hiden objek screen quiz(soal)
     * */
    public void HideQuiz()
    {
        this.gameObject.SetActive(false);
    }
    public void FinishAndNext()
    {

    }
    /* end of implement function */
}
