using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMController : MonoBehaviour
{
    [SerializeField] private float WalkSpeed = 10;
    [SerializeField] private float JumpForce = 5;
    public int count = 0;
    public Text text;
    public AudioClip jump;

    AudioSource source;
    Rigidbody2D rigid;
    private bool isJumping = false;
    private float groundRadius = 0.2f;

    void Start()
    {      
        rigid = gameObject.GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
    
        text.text = count + " of 3 diamonds!";
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rigid.AddForce(new Vector2(0, JumpForce));
            source.clip = jump;
            source.Play();
            
        }

        if (isJumping)
        {
            return;
        }
        else
        {
            float move = Input.GetAxis("Horizontal");

            if (Input.GetAxis("Horizontal") != 0)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    rigid.velocity = new Vector2(move * WalkSpeed, rigid.velocity.y);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                    rigid.velocity = new Vector2(move * WalkSpeed, rigid.velocity.y);
                }
            }
        }
        
       
    }

   

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }
}
