using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private bool inPlay = false;
    [SerializeField] private Vector3 speed;
    [SerializeField] private GameObject ball;
    [SerializeField] private AudioSource ballSource;
    //--
    private float newPositionX;
    private float newPositionY;
    private Transform platform;
    //--
    public Vector3 direction;
    public bool ColisionCooldown;

    private void Awake()
    {
        platform = FindObjectOfType<PlatformController>().transform;
        transform.position = new Vector3(platform.position.x, platform.position.y + 0.7f, 0);
    }

    private void Start()
    {
        speed.z = 0;
        direction.z = 0;
        ColisionCooldown = true;
        speed = new Vector3(30f, 30f, 30f);
    }

    private void Update()
    {
        if (inPlay == false)
        {
           setOnPlatform();
        } else if (inPlay == true)
        {
            moving();
            ColisionCooldown = true;
        }
    }

    void moving ()
    {
        if (newPositionX > GameManager.X + 2)
        {
            direction.x = direction.x * -1;
            ballSource.Play();
        }
        else if (newPositionX < -GameManager.X - 2)
        {
            direction.x = direction.x * -1;
            ballSource.Play();
        }
        if (newPositionY > GameManager.Y)
        {
            direction.y = direction.y * -1;
            ballSource.Play();
        }
        else if (newPositionY < -GameManager.Y - 2)
        {
            Destroy(gameObject);
        }
        newPositionX = transform.position.x + (speed.x * Time.deltaTime) * direction.normalized.x;
        newPositionY = transform.position.y + (speed.y * Time.deltaTime) * direction.normalized.y;
        transform.position = new Vector3(newPositionX, newPositionY, 0);
    }

    void setOnPlatform()
    {
        transform.position = new Vector3(platform.position.x, platform.position.y + 0.9f, 0);
        if (Input.GetButton("Jump"))
        {
            if (FindObjectOfType<PlatformController>().transform.position.x > 0)
            {
                direction.x = 1;
                direction.y = 1;
            }
            if (FindObjectOfType<PlatformController>().transform.position.x <= 0)
            {
                direction.x = -1;
                direction.y = 1;
            }
            inPlay = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.tag == "Platform" || other.tag == "Brick") && inPlay)
        {
            ballSource.Play();
        }
    }

}
