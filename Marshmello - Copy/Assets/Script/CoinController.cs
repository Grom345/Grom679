using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinController : MonoBehaviour {

    public MMController mm;


    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            source.Play();
            mm.count++;
            Destroy(gameObject);
          
        }
    }

   
}
