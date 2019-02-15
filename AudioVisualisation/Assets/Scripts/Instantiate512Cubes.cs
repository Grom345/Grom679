using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour
{
    public GameObject cube;
    public float max;

    GameObject[] cubes = new GameObject[512];

    void Start()
    {
      for (int i = 0; i < 512; i++)
        {
            GameObject instance = (GameObject)Instantiate(cube);
            instance.transform.position = this.transform.position;
            instance.transform.parent = this.transform;
            instance.name = "Cube" + i;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            instance.transform.position = Vector3.forward * 100;
            cubes[i] = instance;
        }
    }

    
    void Update()
    {
        for (int i = 0; i < 512; i++)
        {
            if (cubes != null)
            {
               // cubes[i].transform.localScale = new Vector3(10,(AudioPeer.samples[i] * max) + 2,10);
            }
        }
    }
}
