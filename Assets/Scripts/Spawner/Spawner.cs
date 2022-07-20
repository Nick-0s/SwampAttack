using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Spawner))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private NewWaveTimer _timer;
    [SerializeField] private float _delayBetweenWaves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    public event UnityAction<int, int> NewWaveStarted;
    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;

    public float DelayBetweenWaves => _delayBetweenWaves;

    private void OnEnable()
    {
        SetWave(_currentWaveNumber);
        _player.Died += Stop;
    }

    private void Update()
    {
        if (_currentWave == null)
            return ;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            SpawnEnemy();
            _timeAfterLastSpawn = 0;
        }

        if(_spawned >= _currentWave.Count)
        {
            if(_currentWaveNumber != _waves.Count - 1)
                _timer.StartCountdown();
                
            _currentWave = null;
        }
    }

    private void OnDisable()
    {
        _player.Died -= Stop;        
    }

    public void StartNextWave()
    {
        _spawned = 0;
        _currentWaveNumber++;
        SetWave(_currentWaveNumber);
    }

    private void SpawnEnemy()
    {
        InstantiateEnemy();
        _spawned++;
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Died += _player.OnEnemyDied;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        NewWaveStarted?.Invoke(_currentWaveNumber + 1, _waves.Count);
    }

    private void Stop()
    {
        _currentWave = null;
    }
}

[System.Serializable]
public class Wave
{
    [SerializeField] private GameObject _template;
    [SerializeField] private float _delay;
    [SerializeField] private int _count;

    public GameObject Template => _template;
    public float Delay => _delay;
    public int Count => _count;
}