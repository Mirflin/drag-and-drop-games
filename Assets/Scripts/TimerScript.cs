using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float _currentTime;
    private TMP_Text text;
    void Start()
    {
        _currentTime = 0;
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime = _currentTime + Time.deltaTime;

        TimeSpan time = TimeSpan.FromSeconds(_currentTime);

        text.text = time.Minutes.ToString() + " : "+ time.Seconds.ToString();
    }
}
