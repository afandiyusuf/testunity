using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ProSceneManager : MonoBehaviour {

    public void GotoSelectAvatar()
    {
        SceneManager.LoadScene(1);
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GotoLevelSelect()
    {
        SceneManager.LoadScene(2);
    }
}
