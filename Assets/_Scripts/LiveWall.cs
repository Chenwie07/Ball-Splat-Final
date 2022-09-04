using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveWall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(collision.gameObject.GetComponent<PlayerFxControl>().PlayerDieFx());
        }
    }
}
