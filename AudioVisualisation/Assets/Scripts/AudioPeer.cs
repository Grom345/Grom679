using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    

    public float am, amTwo;
    public  float[] audioBand = new float[8];
    public  float[] audioBandBuffer = new float[8];
    public static float amplitude, amplitudeBuffer;
    [SerializeField] private float audioProfile; 

    float amplitudeHighest;
    float[] bufferDecrease = new float[8];
    float[] freqBandHighest = new float[8];
    float[] samplesLeft = new float[512];
    float[] samplesRight = new float[512];
    float[] freqBand = new float[8];
    float[] bandBuffer = new float[8];
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        AudioProfile(audioProfile);
    }

   
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBand();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
        GetAmplitudeTwo();
        
    }

    void AudioProfile (float audioProfile)
    {
        for (int i = 0; i < 8; i++)
        {
            freqBandHighest[i] = audioProfile;
        }
    }

    void GetAmplitude()
    {
        float currentAmplitude = 0f, currentAmplitudeBuff = 0f;

        for (int i = 0; i < 8; i++)
        {
            currentAmplitude += audioBand[i];
            currentAmplitudeBuff += audioBandBuffer[i];
        }
        if (currentAmplitude > amplitudeHighest)
        {
            amplitudeHighest = currentAmplitude;
        }
        amplitude = currentAmplitude / amplitudeHighest;
        amplitudeBuffer = currentAmplitudeBuff / amplitudeHighest;
        am = amplitudeBuffer;
    }

    void GetAmplitudeTwo()
    {

        amTwo = (audioBandBuffer[0] + audioBandBuffer[1] + audioBandBuffer[2] + audioBandBuffer[3]) / 4;
    }

   
    //returns value between 0 and 1
    void CreateAudioBands ()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBand[i] > freqBandHighest[i])
            {
                freqBandHighest[i] = freqBand[i];
            }
            audioBand[i] = (freqBand[i] / freqBandHighest[i]);
            audioBandBuffer[i] = (bandBuffer[i] / freqBandHighest[i]);
        }
    }

    
    void GetSpectrumAudioSource ()
    {
        source.GetSpectrumData(samplesLeft, 0, FFTWindow.Blackman);
        source.GetSpectrumData(samplesRight, 1, FFTWindow.Blackman);
    }

    //smothing
    void BandBuffer ()
    {
        for (int g = 0; g < 8; ++g)
        {
            if (freqBand[g] > bandBuffer[g])
            {
                bandBuffer[g] = freqBand[g];
                bufferDecrease[g] = 0.005f;
            }
            if (freqBand[g] < bandBuffer[g])
            {
                bandBuffer[g] -= bufferDecrease[g];
                bufferDecrease[g] *= 1.2f;
            }
        }
    }

    //frequency ranges
    void MakeFrequencyBand()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float avarage = 0f;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i==7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                avarage += samplesLeft[count] + samplesRight[count] * (count + 1);
                count++;
            }
            avarage /= count;
            freqBand[i] = avarage * 10;
           
        }
    }
}
