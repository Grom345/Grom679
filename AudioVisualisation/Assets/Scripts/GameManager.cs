using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private Canvas endGame;
    [SerializeField] private PlayerController player;
    [SerializeField] private Text text;
    [SerializeField] private ParticleSystem[] particle;

    private void Start()
    {
        StartCoroutine(GamePlaying());
    }

    IEnumerator GamePlaying ()
    {
        Debug.Log("GameStarting");
        endGame.enabled = false;
        yield return new WaitForSeconds(source.clip.length + 0.75f);
        Debug.Log("GameFinished");
        text.text = "Your score: " + player.score;
        endGame.enabled = true;
        player.enabled = false;
        particle[0].Stop();
        particle[1].Stop();
        particle[2].Stop();
    }

    public void RestartGame ()
    {
        SceneManager.LoadScene(0);
    }

    public void QuiToMenu ()
    {

    }

    public void StartGame ()
    {

    }

    public void QuitTheGame ()
    {
        Application.Quit();
    }
}
