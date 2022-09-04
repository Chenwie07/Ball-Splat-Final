using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTileController : MonoBehaviour
{
    public bool isColored { get; set; }

    private void Start()
    {
        isColored = false;
    }
    internal void ChangeColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
        isColored = true;

        // when you change a tile color check if all tiles are completed. 
        GameManager.instance.CheckLevelComplete(); 
    }
}
