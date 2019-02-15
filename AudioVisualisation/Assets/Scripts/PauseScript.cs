using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private AudioSource source;

    private void Start()
    {
        canvas.enabled = !canvas.enabled;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        if (Time.timeScale == 0)
        {
            canvas.enabled = true;
            source.Pause();
        }else
        {
            canvas.enabled = !canvas.enabled;
            source.Play();
        }
    }
}
