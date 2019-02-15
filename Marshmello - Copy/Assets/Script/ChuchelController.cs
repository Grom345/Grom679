using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuchelController : MonoBehaviour {

    public Sprite chuchel1;
    public Sprite chuchel2;

    SpriteRenderer spRenderer;
    bool isPlayer = true;

	void Start () {
        spRenderer = GetComponent<SpriteRenderer>();
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isPlayer == true)
                isPlayer = false;
            else
                isPlayer = true;

            if (isPlayer)
                spRenderer.sprite = chuchel1;
            else
                spRenderer.sprite = chuchel2;
        }
    }
}
