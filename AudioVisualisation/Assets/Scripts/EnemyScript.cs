using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    
    [SerializeField] private float speed = 10f;

    PlayerController player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
    }


    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
