using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Timer Settings")]
    public Slider _timeSlider;
    [Range(0, 60)] public int levelTime;
    private float maxTime;
    private float currentTime;

    public static Action<float> ACTION_onTimerChange;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = maxTime = levelTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeSlider.value > 0)
        {
            _timeSlider.value = currentTime / maxTime;
            currentTime -= Time.deltaTime;
            ACTION_onTimerChange.Invoke(_timeSlider.value);
        }
        else
            GameManager.instance.GameOver(); 
    }

    private void CheckWarningTime(float time)
    {
        if (time < 0.3)
        {
            _timeSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.red;
        }
    }

    private void OnEnable()
    {
        ACTION_onTimerChange += CheckWarningTime;
    }
    private void OnDisable()
    {
        ACTION_onTimerChange -= CheckWarningTime;

    }
}
