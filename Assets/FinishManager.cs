using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text playerNameText;
    public TMP_Text finalScoreText;      // skor & waktu baru
    public TMP_Text finalTimeText;
    public TMP_Text highScoreText;       // highscore & besttime
    public TMP_Text bestTimeText;

    public Button mainAgainButton;
    public Button backToMenuButton;

    private string playerNameKey = "PlayerName";

    void Start()
    {
        // Ambil nama pemain
        string playerName = PlayerPrefs.HasKey(playerNameKey) ?
                            PlayerPrefs.GetString(playerNameKey) : "Player";
        if (playerNameText != null)
            playerNameText.text = $"Player: {playerName}";

        // Ambil score & time terbaru
        int score = PlayerPrefs.GetInt("Score", 0);
        float time = PlayerPrefs.GetFloat("Time", 0f);
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        if (finalScoreText != null)
            finalScoreText.text = $"Score: {score}";
        if (finalTimeText != null)
            finalTimeText.text = $"Time: {minutes:00}:{seconds:00}";

        // Ambil highscore & besttime terbaik
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        float bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        if (highScoreText != null)
            highScoreText.text = $"Highscore: {highScore}";
        if (bestTimeText != null)
        {
            if (bestTime > 0f)
            {
                int bm = Mathf.FloorToInt(bestTime / 60f);
                int bs = Mathf.FloorToInt(bestTime % 60f);
                bestTimeText.text = $"Best Time: {bm:00}:{bs:00}";
            }
            else
                bestTimeText.text = "Best Time: --:--";
        }

        // Tombol
        if (mainAgainButton != null)
            mainAgainButton.onClick.AddListener(OnMainAgain);
        if (backToMenuButton != null)
            backToMenuButton.onClick.AddListener(OnBackToMenu);
    }

    void OnMainAgain()
    {
        SceneManager.LoadScene("MainScene");
    }

    void OnBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
