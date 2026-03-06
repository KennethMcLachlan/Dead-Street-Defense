using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private int _totalWaves = 10;
    [SerializeField] private int _firstWaveCount = 5;
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _enemyCountMultiplier = 1.5f;
    [SerializeField] private float _spawnDelay = 0.5f;

    private int _currentWave = 0;
    private int _enemiesRemainingInWave = 0;
    private bool _waveActive = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartNextWave());
    }

    private IEnumerator StartNextWave()
    {
        if (_currentWave >= _totalWaves)
        {
            Debug.Log("All waves completed");
            yield break;
        }

        UIManager.Instance.UpdateWaveNumber(_currentWave + 1);

        yield return new WaitForSeconds(_timeBetweenWaves);

        _currentWave++;
        int enemiesToSpawn = Mathf.RoundToInt(_firstWaveCount * Mathf.Pow(_enemyCountMultiplier, _currentWave - 1));
        _enemiesRemainingInWave = enemiesToSpawn;
        _waveActive = true;

        Debug.Log($"Wave {_currentWave} started - spawning {enemiesToSpawn} enemies");

        for (int i= 0; i < enemiesToSpawn; i++)
        {
            SpawnManager.Instance.SpawnEnemy();
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    public void OnEnemyReturnedToPool()
    {
        if (!_waveActive) return;

        _enemiesRemainingInWave--;
        Debug.Log($"Enemies remaining: {_enemiesRemainingInWave}");

        if (_enemiesRemainingInWave <= 0)
        {
            _waveActive = false;
            StartCoroutine(StartNextWave());
        }
    }
}
