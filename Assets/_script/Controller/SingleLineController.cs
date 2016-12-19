using UnityEngine;
//! touch controller
public class SingleLineController : MonoBehaviour {

    public bool valPoint; /*!<cek ter-touch atau tidak*/
	private PuzzleGameManager puzzleGameManager;
    
	// Use this for initialization
	void Start () {
		puzzleGameManager = GameObject.FindGameObjectWithTag (HashTag.QUIZ_ENTITY).GetComponent<PuzzleGameManager> ();
	}
    /**
     * ketika touch puzzleGameManager akan ada reaksi.
     * ketika tidak ada touch tidak ada reaksi.
     * */
	public void onTouch() {

        if (valPoint)
            puzzleGameManager.incrementInteractive();
        else
            puzzleGameManager.incrementFalsePoint();

        GetComponent<BoxCollider> ().enabled = false;
	}
	/*public void BackToDefault(){
	}*/
}
