using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//! game manager dalam puzzle
public class PuzzleGameManager : MonoBehaviour,IMiniGameSoal {

	[System.Serializable]
    //! kumpulan objek line
	public class lineClass
    {
		public GameObject[] ArrLine;/*!<array objek line*/
    }

    public SpriteBank spriteBank;/*!<pemanggilan script spriteBank*/
    public int[] arrPuzzle;/*!<array nomor puzzle*/
    public bool[] arrTrue;/*!<array true/benar*/
    public PiecePuzzleController[] allPiecePuzzle; /*!<array potongan puzzle dari script PiecePuzzleController*/
    public ContainerPuzzleController containerPuzzleController;/*!<pemanggilan script ContainerPuzzleController*/
    public SpriteRenderer DonePuzzle; /*!<sprite render puzzle yang sudah selesai*/
    public lineClass[] AllLine; /*!<semua line pada lineClass*/
    public lineClass[] FalseLine; /*!<line yang salah pada lineClass*/
    public int thisState = 0;/*!<nilai dari kondisi saat ini*/
    public int currentInteactive;/*!<jumlah yang bisa digerakkan*/
    public int interactiveTotal;/*!<jumlah seluruh yang bisa digerakkan*/
    public BoxCollider DrawArea; /*!<area untuk menggambar*/
    public GameObject FinishButton; /*!<objek button finish/selesai*/
    public MainMenuManager mainMenuManager;/*!<pemanggilan script MainMenuManager*/
    public OnScreenDrawing onScreenDrawing;/*!<pemanggilan script OnScreenDrawing*/
    private int totalTrue;
    private int totalFalse;
    public int TruePoint;/*!<point yang benar*/
    public int FalsePoint;/*!<point yang salah*/
    public CoreQuizController coreQuizManager; /*!<pemanggilan script coreQuizController*/

    /* Interface Implementation */
    /**
     * hiden quiz screen
     * */
    public void HideQuiz()
    {
        this.gameObject.SetActive(false);
    }
    /**
     * load/ memunculkan screen quiz
     * */
    public void LoadQuiz(int nomorSoal,CoreQuizController coreQuizManager)
    {
        this.coreQuizManager = coreQuizManager;
        this.gameObject.SetActive(true);
        InitAndRandomPuzzle();
    }
    public void DestroyQuiz()
    {

    }
    public void ResetQuiz()
    {

    }
    /**
     * restart quiz(random quiz ulang)
     * */
    public void RestartQuiz()
    {
        InitAndRandomPuzzle();
    }
    /**
     * selesai game lalu next quiz
     * */
    public void FinishAndNext()
    {
        int varSend = 0;
        if (TruePoint > totalTrue / 2)
        {
            varSend = 1;
        }
        onScreenDrawing.ClearLine();
        coreQuizManager.NextSoal(varSend);
    }
    /* End Of interface implementation */

    public void SendAnswer(bool Condition)
    {
    }
    /**
     * menampilkan score screen
     * */
    public void ShowScoreState()
    {
        

        mainMenuManager.ShowScorePanel(TruePoint.ToString()+"/"+totalTrue, FalsePoint.ToString()+"/"+totalFalse);
    }

    /**
     * random ulang quiz/restart
     * */
	public void InitAndRandomPuzzle()
	{
        
        thisState = 0;
        totalTrue = 0;
        TruePoint = 0;
        FalsePoint = 0;
        totalFalse = 0;
        currentInteactive = 0;
        FinishButton.SetActive(false);
        DrawArea.enabled = false;
        arrPuzzle = new int[9]{0,1,2,3,4,5,6,7,8};
		arrTrue = new bool[]{ false, false, false, false, false, false, false, false, false };
		reshuffle (arrPuzzle);
		for (int i = 0; i < allPiecePuzzle.Length; i++) {
			allPiecePuzzle [i].gameObject.SetActive (true);
			allPiecePuzzle [i].gameObject.GetComponent<BoxCollider> ().enabled = true;
			allPiecePuzzle [i].initPuzzle (arrPuzzle[i], spriteBank.puzzle [0].Piece [arrPuzzle[i]]);
			allPiecePuzzle [i].gameObject.ColorTo (Color.white, 1, 0);
		}
		DonePuzzle.sprite = spriteBank.puzzle [0].DoneSprite;
		containerPuzzleController.ResetContainer ();
		DonePuzzle.gameObject.ColorTo (Color.clear, 0.1f, 0);
	}

	//check if puzzle all correct
    /**
     * mengatur array boolean (arrTrue) pada index.
     * menentukan jumlah true dan false pada arrayTrue.
     * bila benar warna puzzle pada objek DonePuzzle akan berubah putih
     * menampilkan line(garis) pada puzzle
     * */
	public void setArrBool(int index, bool val)
	{
		arrTrue[index] = val;
		for (int i = 0; i < arrTrue.Length; i++) {
			if (arrTrue[i] == false) {
				return;
			}
		}
		for (int i = 0; i < allPiecePuzzle.Length; i++) {
			allPiecePuzzle [i].gameObject.ColorTo (Color.clear, 1, 1);
		}
		DonePuzzle.gameObject.ColorTo (Color.white,1,0);

		Invoke("DisableAllPiece",1.5f );
		StartCoroutine(ShowLineState());

	}

	void DisableAllPiece()
	{
		for(int i=0;i< allPiecePuzzle.Length;i++)
		{
			allPiecePuzzle [i].gameObject.SetActive (false);
		}
	}
    /**
     * jika menggambar line/garis dengan benar maka true point akan bertambah
     * */
	public void incrementInteractive()
	{
		currentInteactive++;
        TruePoint++;
        if (currentInteactive == interactiveTotal) {
			thisState++;
			if (thisState < AllLine.Length) {
				currentInteactive = 0;
				StartCoroutine (ShowLineState ());
			} else {

            }
		}
	}
    /**
     * jika salah line maka false point bertambah
     * */
    public void incrementFalsePoint()
    {
        FalsePoint++;
    }
	IEnumerator ShowLineState()
	{
        FinishButton.SetActive(true);
        DrawArea.enabled = true;
        interactiveTotal = AllLine [thisState].ArrLine.Length;
        totalTrue = interactiveTotal;
        totalFalse = FalseLine[0].ArrLine.Length;

        int i = 0;

		for (i = 0; i < AllLine [thisState].ArrLine.Length; i++) {
			AllLine [thisState].ArrLine [i].GetComponent<BoxCollider> ().enabled = false;
		}
		for (i = 0; i < FalseLine [0].ArrLine.Length; i++) {
			FalseLine [0].ArrLine [i].GetComponent<BoxCollider> ().enabled = false;
		}

		for (i = 0; i < AllLine [thisState].ArrLine.Length; i++) {
			AllLine [thisState].ArrLine [i].SetActive (true);
		}
		for (i = 0; i < FalseLine [0].ArrLine.Length; i++) {
			FalseLine [0].ArrLine [i].SetActive (true);
		}
		for (i = 0; i < AllLine [thisState].ArrLine.Length; i++) {
			AllLine [thisState].ArrLine [i].GetComponent<BoxCollider> ().enabled = true;
		}

		for (i = 0; i < FalseLine [0].ArrLine.Length; i++) {
			FalseLine [0].ArrLine [i].GetComponent<BoxCollider> ().enabled = true;
		}
		yield break;
	}
	void reshuffle(int[] array)
	{
		// Knuth shuffle algorithm :: courtesy of Wikipedia :)
		for (int t = 0; t < array.Length; t++ )
		{
			int tmp = array[t];
			int r = Random.Range(t, array.Length);
			array[t] = array[r];
			array[r] = tmp;
		}
	}
	void DisableColliderAllPuzzle()
	{
		for (int i = 0; i < allPiecePuzzle.Length; i++) {
			allPiecePuzzle [i].gameObject.GetComponent<BoxCollider> ().enabled = false;
		}
	}


}
