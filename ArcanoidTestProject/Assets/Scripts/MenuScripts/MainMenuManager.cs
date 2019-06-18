using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] AudioSource source;

    private void Start()
    {
        scoreText.text = string.Empty + PlayerPrefs.GetInt("maxScore");
    }

    public void startGame()
    {
        StartCoroutine(timerForGame()); 
    }

    public void quitGame()
    {
        StartCoroutine(timerForQuit());
    }

    private IEnumerator timerForGame()
    {
        source.Play();
        yield return new WaitForSecondsRealtime(source.clip.length);
        SceneManager.LoadScene(1);
    }

    private IEnumerator timerForQuit()
    {
        source.Play();
        yield return new WaitForSecondsRealtime(source.clip.length);
        Application.Quit();
    }
}
