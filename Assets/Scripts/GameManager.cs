using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game State")]
    public bool isGameOver = false;
    private float survivalTime;

    [Header("UI References")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finalScoreText;
    public GameObject gameOverPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        gameOverPanel.SetActive(false);
        SoundManager.instance.PlaySound(SoundManager.instance.gameStart);
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        survivalTime += Time.deltaTime;
        timerText.text = "Time: " + survivalTime.ToString("F2");
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        SoundManager.instance.PlaySound(SoundManager.instance.playerHit);
        SoundManager.instance.musicSource.Stop();

        Debug.Log("Game Over!");

        finalScoreText.text = "Time: " + survivalTime.ToString("F2");

        gameOverPanel.SetActive(true);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
