using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveWall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("Should DIe");

            // play player die particle effect. 
            // Destroy(collision.gameObject);
            StartCoroutine(collision.gameObject.GetComponent<PlayerFxControl>().PlayerDieFx()); 
        }
    }
}
