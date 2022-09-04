using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GroundTileController[] allGroundTiles;

    // singleton pattern introduction. 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpNewLevel();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SetUpNewLevel();
    }

    internal void GameOver()
    {
        print("Stop Gameplay");
        SceneManager.LoadScene("Game Over"); 
    }

    private void SetUpNewLevel()
    {
        allGroundTiles = FindObjectsOfType<GroundTileController>();
    }

    internal void CheckLevelComplete()
    {
        bool isComplete = true;
        foreach (var tile in allGroundTiles)
        {
            if (!tile.isColored)
            {
                isComplete = false;
                break;
            }
        }
        if (isComplete)
            NextLevel();
    }

    private void NextLevel()
    {
        print("should call");
        if (SceneManager.GetActiveScene().buildIndex == 4)
            SceneManager.LoadScene(0); // load the first scene, it's game over. 
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
