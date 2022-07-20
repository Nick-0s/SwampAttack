using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Slider Slider;
    [SerializeField] private TMP_Text _display;

    protected string Text = "";

    public void OnValueChanged(int value, int maxValue)
    {
        Slider.value = (float)value / maxValue;
        _display.text = Text + value.ToString() + "/" + maxValue.ToString();
    }
}
