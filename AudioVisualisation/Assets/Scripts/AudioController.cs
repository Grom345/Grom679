using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    public void SetMusicLvl (float Vol)
    {
        mixer.SetFloat("MusicVol", Vol);
    }
 
}
