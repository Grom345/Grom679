using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float h;
    public int score = 0;
    public int count = 10;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Score mainScore;
    [SerializeField] private Spawner spawner;
    [SerializeField] ParticleSystem particle;

    Vector3 movement;
    Rigidbody playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        particle.Stop();
    }

    
    void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        Move(h);
    }

   
    void Move(float h)
    {
        movement.Set(h, 0f, 0f);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);

    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Ball")
        {
            Destroy(other.gameObject);
            mainScore.cmb += 1;
            spawner.count -= 1;
            StartCoroutine(ParticleActive());
            
        }
        if (other.gameObject.tag == "Enemy")
        {
            mainScore.cmb = 0;
            spawner.count -= 1;
        }
    }

    IEnumerator ParticleActive ()
    {
        particle.Play();
        yield return new WaitForSeconds(particle.main.duration);
        particle.Stop();
    }
   
}
