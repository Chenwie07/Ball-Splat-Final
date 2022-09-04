using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [Header("Player Sound Effects")]
    public AudioClip _ballMove;
    public AudioClip _ballHitWall;
    public AudioClip _ballExplodes;

    private AudioSource _source;

    private static SoundEffect instance = null;
    public static SoundEffect Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    internal void PlayPlayerMove()
    {
        _source.PlayOneShot(_ballMove);
    }
    internal void PlayPlayerHitWall()
    {
        _source.PlayOneShot(_ballHitWall);
    }
    internal void PlayPlayerDie()
    {
        _source.PlayOneShot(_ballExplodes);
    }
}
