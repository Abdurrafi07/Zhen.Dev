using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField inputName;     // input nama pemain
    public TMP_Text displayName;         // welcome text
    public TMP_Text highScoreText;       // Highscore
    public TMP_Text bestTimeText;        // Best Time

    private string playerNameKey = "PlayerName";

    void Start()
    {
        // Nama pemain
        if (PlayerPrefs.HasKey(playerNameKey))
        {
            string savedName = PlayerPrefs.GetString(playerNameKey);
            displayName.text = "Welcome back, " + savedName;
        }
        else
        {
            displayName.text = "Enter your name!";
        }

        // Highscore
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "Highscore: " + highScore;

        // Best Time
        float bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        if (bestTime > 0f)
        {
            int minutes = Mathf.FloorToInt(bestTime / 60f);
            int seconds = Mathf.FloorToInt(bestTime % 60f);
            bestTimeText.text = $"Best Time: {minutes:00}:{seconds:00}";
        }
        else
        {
            bestTimeText.text = "Best Time: --:--";
        }
    }

    // Tombol Start
    public void OnStartButton()
    {
        string name = inputName.text;
        if (!string.IsNullOrEmpty(name))
        {
            PlayerPrefs.SetString(playerNameKey, name);
            PlayerPrefs.Save();
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            displayName.text = "Nama tidak boleh kosong!";
        }
    }

    // Tombol Save
    public void OnSaveButton()
    {
        string name = inputName.text;
        if (!string.IsNullOrEmpty(name))
        {
            PlayerPrefs.SetString(playerNameKey, name);
            PlayerPrefs.Save();
            displayName.text = "Saved: " + name;
        }
        else
        {
            displayName.text = "Nama tidak boleh kosong!";
        }
    }

    // Tombol Load
    public void OnLoadButton()
    {
        if (PlayerPrefs.HasKey(playerNameKey))
        {
            string savedName = PlayerPrefs.GetString(playerNameKey);
            inputName.text = savedName;
            displayName.text = "Loaded: " + savedName;
        }
        else
        {
            displayName.text = "Belum ada data tersimpan!";
        }
    }
}
