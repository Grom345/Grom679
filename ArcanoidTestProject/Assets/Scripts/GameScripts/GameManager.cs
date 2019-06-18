using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI panelText;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject button;
    [SerializeField] private AudioSource source;
    //--
    private bool isGameOver;
    //--
    public static float Y;
    public static float X;
    public static int score;
    public static int lastLevel;
    public static int healthP;
    public static bool win;
    public static int maxScore;

    private void Awake()
    {
        loadLevel();
        loadScore();
        panel.SetActive(false);
        isGameOver = false;
    }

    private void Start()
    {
        win = false;
        Debug.Log(win);
        healthP = 2;
        Instantiate(ball, new Vector2(0, 0), Quaternion.identity);
        updateAllText();
        Time.timeScale = 1;
    }

    private void Update()
    {
        checkBall();
        updateAllText();
        if(Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if(Time.timeScale == 1 )
            {
                pause();
            }
            else
            {
                Time.timeScale = 1;
                panel.SetActive(false);
            }
        }
        if(win)
        {
            youWin();
        }
    }

    void checkBall()
    {
        if(GameObject.FindGameObjectWithTag("Ball") == null) 
        {
            if(healthP > 0)
            {
                updateLives(-1);
                Instantiate(ball, new Vector2(0, 0), Quaternion.identity);
            }
            else
            {
                gameOver();
            }       
        }
    }

    public void updateLives(int hpAmount)
    {
        healthP += hpAmount;
        updateAllText();
    }

    public void updateScore(int scoreAmount)
    {
        score += scoreAmount;
        updateAllText();
    }

    void updateAllText()
    {
        scoreText.text = "Score: " + score;//text
        hpText.text = "Health: " + (healthP + 1);//text
    }

    void gameOver()
    {
        gameEnding();
        StartCoroutine(Timer());
        lastLevel = 0;
        saveLevel();
        panelText.text = "Game Over";
        
    }

    void youWin()
    {
        gameEnding();
        StartCoroutine(Timer());
        panelText.text = "You Win";
    }

    void gameEnding()
    {
        Destroy(button);
        pause();
        isGameOver = true;
        score = 0;
        saveScore();
    }

    void pause()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
        timer.text = string.Empty;
        panelText.text = "Pause";
    }

    public void goToMenu()
    {
        StartCoroutine(timerForMenu());
    }

    public static void saveLevel()
    {
        PlayerPrefs.SetInt("lastLevel", lastLevel);
    }

    public static void loadLevel()
    {
        lastLevel = PlayerPrefs.GetInt("lastLevel", 0);
    }

    public static void saveScore()
    {
        PlayerPrefs.SetInt("score", score);
        if(score >= maxScore)
        {
            maxScore = score;
            PlayerPrefs.SetInt("maxScore", maxScore);
        }
    }

    public static void loadScore()
    {
        score = PlayerPrefs.GetInt("score",0);
        maxScore = PlayerPrefs.GetInt("maxScore",0);
    }

    private IEnumerator Timer()
    {
        timer.text = "5";
        yield return new WaitForSecondsRealtime(0.9f);
        timer.text = string.Empty;
        timer.text = "4";
        yield return new WaitForSecondsRealtime(0.9f);
        timer.text = string.Empty;
        timer.text = "3";
        yield return new WaitForSecondsRealtime(0.9f);
        timer.text = string.Empty;
        timer.text = "2";
        yield return new WaitForSecondsRealtime(0.9f);
        timer.text = string.Empty;
        timer.text = "1";
        yield return new WaitForSecondsRealtime(0.9f);
        SceneManager.LoadScene(0);
    }

    private IEnumerator timerForMenu()
    {
        source.Play();
        yield return new WaitForSecondsRealtime(source.clip.length);
        SceneManager.LoadScene(0);
    }
}
