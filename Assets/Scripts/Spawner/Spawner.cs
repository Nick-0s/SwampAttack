using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Spawner))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnTimer _timer;
    [SerializeField] private float _delayBetweenWaves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;
    [SerializeField] private List<Wave> _waves;

    public event UnityAction<int, int> NewWaveStarted;
    private Wave _currentWave;
    private EnemyGroup _currentGroup;
    private int _currentWaveNumber = 0;
    private int _currentGroupNumber;
    private float _timeAfterLastSpawn;
    private int _spawned;

    public float DelayBetweenWaves => _delayBetweenWaves;
    public Player Player => _player;

    private void OnEnable()
    {
        StartWave();
        _player.Died += StopSpawn;
    }

    private void Update()
    {
        if (_currentWave == null)
            return ;

        _timeAfterLastSpawn += Time.deltaTime;

        {
        if (_timeAfterLastSpawn >= _currentGroup.DelayBetweenEnemies)
            SpawnEnemy();

            if(_spawned == _currentGroup.Count)
                TryStartNextGroup();
        }
    }

    private void OnDisable()
    {
        _player.Died -= StopSpawn;        
    }

    public void StartWave()
    {
        _currentWave = _waves[_currentWaveNumber];

        _currentGroupNumber = 0;
        _currentGroup = _currentWave.GetGroup(_currentGroupNumber);

        NewWaveStarted?.Invoke(_currentWaveNumber + 1, _waves.Count);
    }

    public void StartGroup()
    {
        _currentGroup = _currentWave.GetGroup(_currentGroupNumber);
    }

    private void TryStartNextGroup()
    {
        _spawned = 0;
        _currentGroupNumber++;

        if (_currentGroupNumber != _currentWave.GroupsCount)
            StartCountdownToNextGroup();
        else
            TryStartNextWave();
    }

    private void TryStartNextWave()
    {
        _currentWaveNumber++;

        if(_currentWaveNumber != _waves.Count)
            _timer.StartCountdown(_delayBetweenWaves, StartWave, true);
                    
        StopSpawn();
    }

    private void StartCountdownToNextGroup()
    {
        _timer.StartCountdown(_currentGroup.DelayBeforeSpawn, StartGroup);
    }

    private void SpawnEnemy()
    {
        InstantiateEnemy();
        _spawned++;
        _timeAfterLastSpawn = 0;
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentGroup.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Died += _player.OnEnemyDied;
    }

    private void StopSpawn()
    {
        _currentWave = null;
    }
}

[System.Serializable]
public class Wave
{
    [SerializeField] private List<EnemyGroup> _groups;


    public int GroupsCount => _groups.Count;

    public EnemyGroup GetGroup(int index) => _groups[index];
}

[System.Serializable]
public class EnemyGroup
{
    [SerializeField] private GameObject _template;
    [SerializeField] private float _delayBeforeSpawn;
    [SerializeField] private float _delayBetweenEnemies;
    [SerializeField] private int _count;

    public GameObject Template => _template;
    public float DelayBeforeSpawn => _delayBeforeSpawn;
    public float DelayBetweenEnemies => _delayBetweenEnemies;
    public int Count => _count;
}