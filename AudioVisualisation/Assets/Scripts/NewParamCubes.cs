using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewParamCubes : MonoBehaviour
{
    public float startScale, maxScale;
    public int band;

    public bool useBuffer;
   Material material;
    public AudioPeer audiop;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!useBuffer)
        {
           transform.localScale = new Vector3(transform.localScale.x, (audiop.audioBand[band] * maxScale) + startScale, transform.localScale.z);
            Color color = new Color(audiop.audioBand[band], audiop.audioBand[band], audiop.audioBand[band]);
            material.SetColor("_EmissionColor", color);
        }
        else
        {
           transform.localScale = new Vector3(transform.localScale.x, (audiop.audioBandBuffer[band] * maxScale) + startScale, transform.localScale.z);
            Color color = new Color(audiop.audioBandBuffer[band] / 2, audiop.audioBandBuffer[band] / 2, audiop.audioBandBuffer[band] / 2);
            material.SetColor("_EmissionColor", color);
        }

    }
}
