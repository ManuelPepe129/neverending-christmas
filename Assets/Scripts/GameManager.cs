using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float dayDuration = 10.0f;
    private float _dayDuration;
    private float _currentTime;
    private Coroutine _dayTimerCoroutine;

    // Gifts management
    [SerializeField] private GameObject[] giftSpawnPositions;
    [SerializeField] private GameObject giftPrefab;

    // Enemies management
    [SerializeField] private GameObject[] enemySpawnPositions;
    [SerializeField] private GameObject[] enemyPrefabs;

    // Gifts management
    private int _numberOfGiftsToSpawn = 3;
    private int _giftsCollected = 0;

    // Enemies management
    private int _numberOfEnemiesToSpawn;
    private const int EnemyBaseNumber = 5;
    private const float EnemyIncrementFactor = 1.55f;
    public int enemiesSpawned = 0;

    //Timer management
    [SerializeField] private AudioSource timerAudioSource;
    private bool _timerSoundPlayed = false;
    private float _timerSoundDuration = 13;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        // Start timer
        _dayDuration = dayDuration * 60f; // Convert minutes to seconds
        _currentTime = 0f;
        _dayTimerCoroutine = StartCoroutine(DayTimer());

        SpawnGifts();

        _numberOfEnemiesToSpawn = EnemyBaseNumber;
        InvokeRepeating(nameof(SpawnEnemies), 0.5f, 5f);
    }

    private void SpawnGifts()
    {
        var tmpGiftSpawnPositions = new List<GameObject>(giftSpawnPositions);
        if (_numberOfGiftsToSpawn > giftSpawnPositions.Length) _numberOfGiftsToSpawn = giftSpawnPositions.Length;
        var giftsSpawned = 0;
        while (giftsSpawned < _numberOfGiftsToSpawn)
        {
            var positionIndex = Random.Range(0, tmpGiftSpawnPositions.Count);
            var spawnPosition = tmpGiftSpawnPositions[positionIndex].transform.position;
            Instantiate(giftPrefab, spawnPosition, Quaternion.identity);
            tmpGiftSpawnPositions.RemoveAt(positionIndex);
            giftsSpawned++;
        }
    }

    private void SpawnEnemies()
    {
        var tmpEnemySpawnPositions = new List<GameObject>(enemySpawnPositions);
        if (_numberOfEnemiesToSpawn > enemySpawnPositions.Length) _numberOfEnemiesToSpawn = enemySpawnPositions.Length;
        while (enemiesSpawned < _numberOfEnemiesToSpawn)
        {
            var positionIndex = Random.Range(0, tmpEnemySpawnPositions.Count);
            var spawnPosition = tmpEnemySpawnPositions[positionIndex].transform.position;
            Instantiate(enemyPrefabs[_giftsCollected], spawnPosition, Quaternion.identity);
            tmpEnemySpawnPositions.RemoveAt(positionIndex);
            enemiesSpawned++;
        }
    }

    private IEnumerator DayTimer()
    {
        while (_currentTime <= _dayDuration)
        {
            _currentTime += Time.deltaTime;
            if(_currentTime >= (_dayDuration-_timerSoundDuration))
            {
                if (!_timerSoundPlayed)
                {
                    _timerSoundPlayed = true;
                    timerAudioSource.Play();
                }
            }
            yield return null;
        }

        StartCoroutine(LoadSceneCoroutine(2));
    }

    public void OnGiftCollected()
    {
        _giftsCollected++;

        // TODO: empowerment

        _numberOfEnemiesToSpawn = Mathf.RoundToInt(EnemyBaseNumber * Mathf.Pow(EnemyIncrementFactor, _giftsCollected));
        SpawnEnemies();

        if (_giftsCollected == _numberOfGiftsToSpawn)
        {
            SpawnBossWave();
        }
    }

    private void SpawnBossWave()
    {
        throw new NotImplementedException("SpawnBossWave not implemented yet.");
    }

    public void OnPlayerDeath(bool isDeath)
    {
        // TODO: Play Game Over audio
        StopCoroutine(_dayTimerCoroutine);
        Time.timeScale = 0; // Freeze the game
        StartCoroutine(LoadSceneCoroutine(2));
    }

    public void OnLevelCompleted()
    {
        // TODO: Play Win audio
        StopCoroutine(_dayTimerCoroutine);
        Time.timeScale = 0; // Freeze the game
        // Load Win Scene
        StartCoroutine(LoadSceneCoroutine(3));
    }

    public float GetNormalizedTime()
    {
        return 1.0f - _currentTime / _dayDuration;
    }

    public float GetRemainingTime()
    {
        return _dayDuration - _currentTime;
    }

    private IEnumerator LoadSceneCoroutine(int sceneBuildIndex)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);
        while (asyncLoad is { isDone: false })
        {
            yield return null;
        }
    }
}