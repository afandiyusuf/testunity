using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
//! quiz manager
public class CoreQuizController : MonoBehaviour {
    public GameObject[] AllMiniGameGO;/*!<kumpulan beberapa soal yang akan muncul pada battle scene*/
    public List<IMiniGameSoal> AllMiniGameSoal;/*!<menggambil soal dari seluruh jumlah soal yang ada*/
    public int NomorSoal = 0; /*!<nomor soal dari AllMiniGameGO*/
    public int PeakPoint = 20; /*!<total point yang harus dikumpulkan*/
    public float TruePoint = 10; /*!<total point benar player*/
    public float FalsePoint = 10; /*!<total point salah player*/
    private BarController barUI;

	public GameObject loadingScreen; /*!<image loading game*/
    public GameObject winScreen; /*!<image menang*/
    public GameObject loseScreen; /*!<image kalah*/
    //bg transparan ketika game selesai
    public GameObject bgEndBattle; /*!<image background ketika battle selesai*/

    public GameObject playerImage; /*!<image player*/
    public GameObject enemyImage; /*!<image musuh*/
    //random sprite buah di soal dan jawaban
    public randomSpriteSoal rss; /*!<memanggil script randomSproteSoal*/

    bool hasil;
    void Start()
    {
		loadingScreen.gameObject.SetActive(false);
		winScreen.gameObject.SetActive(false);
		loseScreen.gameObject.SetActive(false);
        //bg transparan ketika game selesai
        bgEndBattle.gameObject.SetActive(false);

        barUI = GameObject.FindGameObjectWithTag(HashTag.BAR_ENTITY).GetComponent<BarController>();
        AllMiniGameGO = GameObject.FindGameObjectsWithTag(HashTag.QUIZ_ENTITY);
        AllMiniGameSoal = new List<IMiniGameSoal>();

        for (int i=0;i<AllMiniGameGO.Length;i++)
        {
            AllMiniGameSoal.Add(AllMiniGameGO[i].GetComponent<IMiniGameSoal>());
            AllMiniGameSoal[i].HideQuiz();
        }
        NextSoal(99);
    }

    private void HideAllQuiz()
    {
        for (int i=0;i<AllMiniGameGO.Length;i++)
        {
            AllMiniGameSoal[i].HideQuiz();
        }

    }

    /**
     * soal selanjutnya ketika player selesai menjawab.
     * mendetect jawaban benar atau salah.
     * jika benar TruePoint bertambah FalsePoint Berkurang.
     * jika salah FalsePoint Bertambah TruePoint berkurang.
     * penambahan atau pengurangan TruePoint dan FalsePoint bergantung pada nomorSoal semakin besar nomorSoal semakin besar pula pertambahan atau pengurangan point.
     * jika truePoint atau falsePoint sama dengan PeakPoint maka permainan selesai.
     * */
    public void NextSoal(int isTrue=99)
    {
        
        System.GC.Collect();
        NomorSoal++;
        HideAllQuiz();

        if (isTrue != 99 && isTrue != 1 && isTrue != 0)
        {
            Debug.LogError("isTrue must be 1 || 0 || 99");
            //random sprite buah
            rss.ChangeRandom();
        }
        
        if (isTrue != 99)
        {
            if (isTrue == 1)
            {
				enemyImage.gameObject.GetComponent<Animator>().SetBool("isHit", true);
				StartCoroutine ("ChangeAniState", enemyImage);

                TruePoint += NomorSoal;
                FalsePoint -= NomorSoal;
				SoundManager.instance.PlayCorrectSound();
                //random sprite buah
                rss.ChangeRandom();
            }
            else if(isTrue == 0)
            {
				playerImage.gameObject.GetComponent<Animator>().SetBool("isHit", true);
				StartCoroutine ("ChangeAniState", playerImage);

                TruePoint -= NomorSoal;
                FalsePoint += NomorSoal;
				SoundManager.instance.PlayIncorrectSound();
                //random sprite buah
                rss.ChangeRandom();
            }
        }

        barUI.ChangeValBar(TruePoint / PeakPoint);
        Debug.Log(TruePoint / PeakPoint);

        //Cek apakah point sudah mencapai peak (puncak)
        if (TruePoint >= PeakPoint)
        {
			winScreen.gameObject.SetActive(true);
            //bg transparan ketika game selesai
            bgEndBattle.gameObject.SetActive(true);
            //Invoke("LoadingScene", 2f);
            Debug.Log("WIN");

        }
        else if (FalsePoint >= PeakPoint)
        {
			loseScreen.gameObject.SetActive(true);
            //bg transparan ketika game selesai
            bgEndBattle.gameObject.SetActive(true);
            //Invoke("LoadingScene", 2f);
            Debug.Log("LOSE");

        }
        else
        {
            //generating jenis soal yang ingin dipanggil
            int RandomSoal = Random.Range(0, AllMiniGameSoal.Count);

            Debug.Log("initiate soal jenis " + RandomSoal);  

            AllMiniGameSoal[RandomSoal].LoadQuiz(NomorSoal, this);
        }

    }

	void LoadingScene()
	{
		loadingScreen.gameObject.SetActive(true);
		Invoke("ChangeScene", 1f);
	}

	void ChangeScene()
	{
		SceneManager.LoadScene("Demo_area1");		
	}

    /**
     * ketika battle selesai akan langsung masuk loadingScene.
     * */
    public void EndBattle()
    {
        Invoke("LoadingScene", 2f);
    }

    /**
     * reset battle ketika pemain kalah dan press button "coba lagi".
     * */
    public void ResetBattle()
    {
        NomorSoal = 0;
        PeakPoint = 20;
        TruePoint = 10;
        FalsePoint = 10;
        NextSoal();
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        bgEndBattle.SetActive(false);
    }

	IEnumerator ChangeAniState(GameObject hurtedSide)
	{
		yield return new WaitForSeconds(0.5f);
		hurtedSide.gameObject.GetComponent<Animator>().SetBool("isHit", false);	
	}
}
