using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the UI Text for the timer
    public float startTime = 60f; // Start time in seconds (1 minute)
    private float currentTime;

    public TileMapManager tileMapManager; // Reference to the TileMapManager script
    public GameObject gameOverPanel; // Reference to the Game Over panel
    public TextMeshProUGUI winnerText; // Reference to the Winner Text

    private bool isGameOver = false;

    private void Start()
    {
        currentTime = startTime;
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (isGameOver) return; // Stop the timer if the game is over

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            EndGame();
        }

        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void EndGame()
    {
        isGameOver = true;

        // Determine the winner
        float player1Percentage = tileMapManager.GetPlayer1Percentage();
        float player2Percentage = tileMapManager.GetPlayer2Percentage();

        if (player1Percentage > player2Percentage)
        {
            winnerText.text = "GOO WINS";
        }
        else if (player2Percentage > player1Percentage)
        {
            winnerText.text = "HUE WINS";
        }
        else
        {
            winnerText.text = "DRAW";
        }

        // Activate the Game Over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }
}