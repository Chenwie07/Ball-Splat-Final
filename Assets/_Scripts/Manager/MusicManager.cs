using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource gameMusicSource;

    private static MusicManager instance = null;
    public static MusicManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        gameMusicSource = GetComponent<AudioSource>();

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
        if (!gameMusicSource.isPlaying)
        {
            gameMusicSource.Play();
        }
    }

}
