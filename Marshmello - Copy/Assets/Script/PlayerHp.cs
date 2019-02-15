using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHp : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public AudioClip[] clips;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public float time = 3f;

    AudioSource playerAudio;
    MMController mmController;
    bool isDead;
    bool damaged;

    void Awake()
    {
       playerAudio = GetComponent<AudioSource>();
        mmController = GetComponent<MMController>();
        currentHealth = startingHealth;
    }

   
    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
        if (transform.position.y <= -6f)
        {
            Death();
        }
    }
    bool isTeek = false;
    IEnumerator DeathCourutine(float time)
    {
        isTeek = true;
        //Death();
        yield return new WaitForSecondsRealtime(time);
        RestartLevel();
        mmController.enabled = false;
        isTeek = false;
    }

    public void TakeDamage(int amount)
    {
        int randomIndex = Random.Range(0, clips.Length);
        
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.clip = clips[randomIndex];
        playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
        
    }


    void Death()
    {
        isDead = true;
        playerAudio.clip = deathClip;
        playerAudio.Play();
        if (!isTeek) StartCoroutine(DeathCourutine(deathClip.length));
        
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }

   
}
