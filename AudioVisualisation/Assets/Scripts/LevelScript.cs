using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{

    public AudioPeer audiop;
    [SerializeField] private int band;
    [SerializeField] private bool useBuffer;
    [SerializeField] private GameObject[] objects;

   
    Transform start;
    Transform startPoint;
    Material material;
  

    void Start()
    {
       
        material = GetComponent<MeshRenderer>().materials[0];
        start = GameObject.Find("Start").transform;
    }

    
    void Update()
    { 

        if (!useBuffer)
        {
            Color color = new Color(audiop.audioBand[band], audiop.audioBand[band], audiop.audioBand[band]);
            material.SetColor("_EmissionColor", color);
        }
        else
        {
            Color color = new Color(audiop.audioBandBuffer[band] / 2, audiop.audioBandBuffer[band] / 2, audiop.audioBandBuffer[band] / 2);
            material.SetColor("_EmissionColor", color);
            
            /*if (band == 2 || band == 3 || band == 4)
            {
               if ()
                {
                    startPoint = start.GetChild(band);
                    Instantiate(objects[0], startPoint.transform.position, startPoint.transform.rotation);
                }
                /*if (audiop.audioBandBuffer[band] >= 0.9f && audiop.audioBandBuffer[band] <= .91f  || 
                    audiop.audioBandBuffer[band] >= 0.1f && audiop.audioBandBuffer[band] <= .11f)
                {
                    startPoint = start.GetChild(band);
                    Instantiate(objects[1], startPoint.transform.position, startPoint.transform.rotation);
                }

            }*/
            

        }

    }

   
}
