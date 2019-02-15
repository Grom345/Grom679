using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScriptLow : MonoBehaviour
{

    public AudioPeer audiop;
    [SerializeField] private int band;
    [SerializeField] private bool useBuffer;


    Material material;

    void Start()
    {
        
        material = GetComponent<MeshRenderer>().materials[0];
        
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
            
        }

    }
}
