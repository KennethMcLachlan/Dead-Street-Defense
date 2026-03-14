using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private int _totalWaves = 10;
    [SerializeField] private int _firstWaveCount = 5;
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _enemyCountMultiplier = 1.5f;
    [SerializeField] private float _minSpawnDelay = 0.4f;
    [SerializeField] private float _maxSpawnDelay = 1.2f;

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
            UIManager.Instance.EngageWinGame();
            // Add SFX
            yield break;
        }

        yield return new WaitForSeconds(3f);

        UIManager.Instance.EngageWaveInfo();
        yield return new WaitForSeconds(5f);
        UIManager.Instance.SetWaveStartText();
        yield return new WaitForSeconds(2f);
        UIManager.Instance.DisengageWaveInfo();

        UIManager.Instance.UpdateWaveNumber(_currentWave + 1);

        if (_currentWave <= 1)
        {
            yield return new WaitForSeconds(5f);
        }

        _currentWave++;
        int enemiesToSpawn = Mathf.RoundToInt(_firstWaveCount * Mathf.Pow(_enemyCountMultiplier, _currentWave - 1));
        _enemiesRemainingInWave = enemiesToSpawn;
        _waveActive = true;

        Debug.Log($"Wave {_currentWave} started - spawning {enemiesToSpawn} enemies");

        for (int i= 0; i < enemiesToSpawn; i++)
        {
            SpawnManager.Instance.SpawnEnemy(_currentWave >= 4);
            yield return new WaitForSeconds(Random.Range(_minSpawnDelay, _maxSpawnDelay));
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
