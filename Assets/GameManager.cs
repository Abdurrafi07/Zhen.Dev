using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text scoreText;
    public TMP_Text timerText;

    private int score = 0;
    private float elapsedTime = 0f;
    private bool isGameRunning = true;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // tetap hidup antar scene
    }

    void Update()
    {
        if (!isGameRunning) return;

        elapsedTime += Time.deltaTime;
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        if (timerText != null)
            timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }

    // ===========================================
    // Save Data terakhir
    public void SaveData()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetFloat("Time", elapsedTime);
        PlayerPrefs.Save();

        // Update highscore & besttime
        CheckHighScore();
        CheckBestTime();
    }

    public int GetSavedScore() => PlayerPrefs.GetInt("Score", 0);
    public float GetSavedTime() => PlayerPrefs.GetFloat("Time", 0f);

    // ===========================================
    // Highscore & Best Time
    public void CheckHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    public void CheckBestTime()
    {
        float bestTime = PlayerPrefs.GetFloat("BestTime", Mathf.Infinity);
        if (elapsedTime < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", elapsedTime);
            PlayerPrefs.Save();
        }
    }

    public int GetHighScore() => PlayerPrefs.GetInt("HighScore", 0);
    public float GetBestTime() => PlayerPrefs.GetFloat("BestTime", 0f);
}
