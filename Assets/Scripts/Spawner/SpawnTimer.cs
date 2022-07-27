using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Spawner))]
public class SpawnTimer : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _newWaveIndicator;

    private Spawner _spawner;
    private Player _player;
    private Coroutine _countdown;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _player = _spawner.Player;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        _player.Died += Stop;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        _player.Died -= Stop;

    }

    public void StartCountdown(float delay, Action functionOnFinish, bool withIndicator = false)
    {
        _countdown = StartCoroutine(Countdown(delay, functionOnFinish, withIndicator ? _newWaveIndicator : null));
    }

    private IEnumerator Countdown(float delay, Action functionOnFinish, Image image = null)
    {
        float timer = delay;

        if(image != null)
            image.gameObject.SetActive(true);

        while(timer > 0)
        {
            if(image != null)
                image.fillAmount = timer / delay;

            timer -= Time.deltaTime;
            yield return null;
        }

        if(image != null)
            image.gameObject.SetActive(false);

        functionOnFinish();
    }

    private void OnButtonClick()
    {
        if(_countdown != null)
            StopCoroutine(_countdown);

        StartNextWave();
    }

    private void StartNextWave()
    {
        _spawner.StartWave();
    }

    private void Stop()
    {
        if(_countdown != null)
            StopCoroutine(_countdown);

        _newWaveIndicator.gameObject.SetActive(false);
    }
}
