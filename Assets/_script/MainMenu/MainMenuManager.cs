using UnityEngine;
using UnityEngine.UI;
//! pengaturan mainMenu
public class MainMenuManager : MonoBehaviour {

    public GameObject AllMainMenuPanel;/*!<semua objek panel main menu*/
    public GameObject MainMenuPanel;/*!<objek panel main menu*/
    public PuzzleGameManager puzzleGameManager;/*!<pemanggilan script PuzzleGameManager*/

    public GameObject ScorePanel;/*!<objek panel score*/
    public Text benarText;/*!<teks benar*/
    public Text salahText;/*!<teks salah*/
    /**
     * menampilkan main menu.
     * hiden semua panel kecuali main menu
     * */
    public void ShowMainMenu()
    {
        AllMainMenuPanel.SetActive(true);
        HideAllPanel();
        MainMenuPanel.SetActive(true);
    }
    /**
     * memulai puzzle game
     * hiden semua panel main menu
     * */
    public void RestartPuzzleGame()
    {
        puzzleGameManager.InitAndRandomPuzzle();
        AllMainMenuPanel.SetActive(false);
    }
    /**
     * menampilkan panel score
     * hiden semua panel kecuali panel score
     * */
    public void ShowScorePanel(string BenarScore, string SalahScore)
    {
        AllMainMenuPanel.SetActive(true);
        HideAllPanel();

        ScorePanel.SetActive(true);
        benarText.text = BenarScore;
        salahText.text = SalahScore;
    }

    private void HideAllPanel()
    {
        MainMenuPanel.SetActive(false);
        ScorePanel.SetActive(false);
    }
}
