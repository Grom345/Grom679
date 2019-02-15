using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Light))]
public class Light : MonoBehaviour
{
    public float startScale, scaleMultiplier;
    public int band;

    Light _light;
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light> ();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
