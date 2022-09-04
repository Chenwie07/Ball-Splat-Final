using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static AudioSource CreateAudioSource(this MonoBehaviour parent, AudioClip clip, bool startImmediately)
    {
        GameObject bp_source = new GameObject(); 
        bp_source.transform.SetParent(parent.transform);
        bp_source.transform.position = parent.transform.position;
        AudioSource newSource = bp_source.AddComponent<AudioSource>() as AudioSource; 
        newSource.clip = clip;
        if (startImmediately)
            newSource.Play();
        return newSource; 
    }
}
