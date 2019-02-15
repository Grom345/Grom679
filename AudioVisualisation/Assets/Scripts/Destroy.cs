using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
   [SerializeField] private PlayerController player;
   [SerializeField] private Score mainScore;
    [SerializeField] private Spawner spawner;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Ball")
        {
            player.count = 10;
           
            Destroy(other.gameObject);
            spawner.count -= 1;
        }
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            spawner.count -= 1;
        }
    }
}
