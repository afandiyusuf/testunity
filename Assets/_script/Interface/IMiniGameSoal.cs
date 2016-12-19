//! interface soal pada game
public interface IMiniGameSoal {
    void LoadQuiz(int nomorSoal,CoreQuizController coreQuizManager);
    void DestroyQuiz();
    void ResetQuiz();
    void RestartQuiz();
    void HideQuiz();
    void FinishAndNext();
    void SendAnswer(bool answerContidion);
}
