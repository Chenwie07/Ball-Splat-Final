using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFxControl : MonoBehaviour
{

    [SerializeField] ParticleSystem _playerDie;

    internal IEnumerator PlayerDieFx()
    {
        _playerDie.Play();
        SoundEffect.Instance.PlayPlayerDie();
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitUntil(() => !_playerDie.isPlaying);
        gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);
        //MusicManager.Instance.GetComponent<AudioSource>().Stop();
        GameManager.instance.GameOver();
    }
}
