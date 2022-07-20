using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressBar : Bar
{
    [SerializeField] private Spawner _spawner;
    

    private void OnEnable()
    {
        _spawner.NewWaveStarted += OnValueChanged;
        Slider.value = 0;
        Text = "Wave: ";
    }

     private void OnDisable()
     {
        _spawner.NewWaveStarted -= OnValueChanged;
     }
}
