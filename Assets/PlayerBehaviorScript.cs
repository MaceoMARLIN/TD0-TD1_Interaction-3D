using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;



public class PlayerBehaviorScript : MonoBehaviour
{
    private float timer = 0f;
    private bool isTimerRunning = false;
    private bool isGameOver = false;
    private Behaviour playerController;
    public TMPro.TextMeshProUGUI timerText;
    private int laserCount = 0;
    public TMPro.TextMeshProUGUI laserCountText;
    public Canvas canvas_gameOver;
    public Canvas canvas_gamePlaying;
    public TMPro.TextMeshProUGUI gameOverTimer;
    public TMPro.TextMeshProUGUI gameOverLaserCount;
    public TMPro.TextMeshProUGUI scoreText;
    private int score = 0;
    private int timescore = 0;
    private int laserscore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canvas_gamePlaying.enabled = true;
        canvas_gameOver.enabled = false;
        playerController = FindObjectOfType<FirstPersonController>() as Behaviour ?? FindObjectOfType<RigidbodyFirstPersonController>() as Behaviour;
    }

    // Update is called once per frame
    void Update()
    {
        laserCountText.text = "Laser Count: " + laserCount;
        if (Input.anyKeyDown)
        {
            isTimerRunning = true;
        }
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            timerText.text = "Timer: " + timer.ToString("F2") + "s";
        }

        if (isGameOver)
        {
            ScoreGame();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            laserCount++;
            laserCountText.text = "Laser Count: " + laserCount;
        }

        if (other.CompareTag("Exit"))
        {
            isGameOver = true;
            if (playerController != null)
            {
                playerController.enabled = false;
            }
            Time.timeScale = 0f; // Stop the game
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Make the cursor visible
            canvas_gamePlaying.enabled = false;
            canvas_gameOver.enabled = true;
            gameOverTimer.text = "Your time: " + timer.ToString("F2") + "s";
            gameOverLaserCount.text = "Your laser count: " + laserCount;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    private void ScoreGame()
    {
        if (laserCount <= 5)
        {
            laserscore = 6 - laserCount;
        }
        else
        {
            laserscore = 0;
        }

        if (timer <= 60f)
        {
            timescore = 10 - Mathf.FloorToInt(timer / 6f);
        }
        else
        {
            timescore = 0;
        }

        score = laserscore + timescore + 4;
        scoreText.text = "Score : " + score;
    }
}