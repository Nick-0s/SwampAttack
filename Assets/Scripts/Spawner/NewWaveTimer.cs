using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Spawner))]
public class NewWaveTimer : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    private Spawner _spawner;
    private Coroutine _countdown;


    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void StartCountdown()
    {
        _image.gameObject.SetActive(true);
        _countdown = StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        float timer = _spawner.DelayBetweenWaves;

        while(timer > 0)
        {
            _image.fillAmount = timer / _spawner.DelayBetweenWaves;
            timer -= Time.deltaTime;
            yield return null;
        }

        StartNextWave();
    }

    private void OnButtonClick()
    {
        if(_countdown != null)
            StopCoroutine(_countdown);

        StartNextWave();
    }

    private void StartNextWave()
    {
        _image.gameObject.SetActive(false);
        _spawner.StartNextWave();
    }
}
