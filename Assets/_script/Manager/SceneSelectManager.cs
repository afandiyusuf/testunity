using UnityEngine;
using UnityEngine.SceneManagement;
//! scene manager (pemilihan scene)
public class SceneSelectManager : MonoBehaviour {
	/**
     * menuju secene Demo_petabesar
     * */
	public void LoadMap()
	{
		SceneManager.LoadScene("Demo_petabesar");	
	}

    /**
     * menuju secene quiz 
     * */
    public void LoadQuiz()
    {
        SceneManager.LoadScene(HashTag.QUIZ_SCENE);
    }

    /**
     * menuju secene puzzle
     * */
    public void LoadPuzzle()
    {
        SceneManager.LoadScene(HashTag.PUZZLE_SCENE);
    }

    /**
     * menuju secene all_quiz
     * */
    public void LoadAllScene()
    {
        SceneManager.LoadScene(HashTag.ALL_QUIZ_SCENE);
    }

    
}
