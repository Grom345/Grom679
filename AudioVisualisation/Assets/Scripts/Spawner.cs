using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public AudioPeer audiop;
    public int count = 0;
    public int randomIndexParticle;
    [SerializeField] private float spawnThreshold = 0.5f;
    [SerializeField] private int frequency = 0;
    [SerializeField] private FFTWindow fftWindow;
    [SerializeField] private GameObject[] objects;
    [SerializeField] private AudioSource source;
    [SerializeField] private ParticleSystem[] particle;

    Transform start;
    Transform startPoint;
    Material material;
    private float[] samplesRight = new float[1024];
    private float[] samplesLeft = new float[1024];
    private float[] samples = new float[1024];
  

    void Start()
    { 
        start = GameObject.Find("Start").transform;
        particle[0].Stop();
        particle[1].Stop();
        particle[2].Stop();
    }


    void Update()
    {

        source.GetSpectrumData(samplesRight, 0, fftWindow);
        source.GetSpectrumData(samplesLeft, 1, fftWindow);

        for (int i = 0; i < 1024; i++)
        {
            samples[i] = (samplesLeft[i] + samplesRight[i]) / 2;
        }

      

        if (samples[frequency] > spawnThreshold && count <=25)
        {
            int randomIndex = Random.Range(2,5);
            int randomIndexObjects = Random.Range(0,2);
            if (randomIndex == 2)
            {
                randomIndexParticle = 0;
            } else if (randomIndex == 3)
            {
                randomIndexParticle = 1;
            } else
            {
                randomIndexParticle = 2;
            }
            StartCoroutine(ParticleActivity());
            startPoint = start.GetChild(randomIndex);
            Instantiate(objects[randomIndexObjects], startPoint.transform.position, startPoint.transform.rotation);
            count += 1; 
        }

    }

    IEnumerator ParticleActivity ()
    {
        particle[randomIndexParticle].Play();
        yield return new WaitForSeconds(particle[randomIndexParticle].main.duration);
        particle[randomIndexParticle].Stop();
    }
}
