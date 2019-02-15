using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BallScript : MonoBehaviour
{

    

    [SerializeField] private float speed = 10f;

    PlayerController player;
    AudioPeer audiop;
    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        audiop = GameObject.FindObjectOfType<AudioPeer>();
    }


    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    

}
