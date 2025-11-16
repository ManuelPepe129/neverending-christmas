using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private int numberOfGiftsToSpawn = 3;
    
    // Enemies management
    [SerializeField] private GameObject[] enemySpawnPositions;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int numberOfEnemiesToSpawn = 3;
    
    // Gifts management
    private int _totalGifts = 0;
    private int _giftsCollected = 0;
    
    // Enemies management
    private int _totalEnemies = 0;

    private void Start()
    {
        // TODO: Move to UI
        StartGame();
    }

    private void StartGame()
    {
        // Start timer
        _dayDuration = dayDuration * 60f; // Convert minutes to seconds
        _currentTime = 0f;
        _dayTimerCoroutine = StartCoroutine(DayTimer());
        
        SpawnGifts();
        
        // TODO: Start Waves
        SpawnEnemies();
    }

    private void SpawnGifts()
    {
        var tmpGiftSpawnPositions = new List<GameObject>(giftSpawnPositions);
        if (numberOfGiftsToSpawn > giftSpawnPositions.Length) numberOfGiftsToSpawn = giftSpawnPositions.Length;
        var giftsSpawned = 0;
        while (giftsSpawned < numberOfGiftsToSpawn)
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
        if (numberOfEnemiesToSpawn > enemySpawnPositions.Length) numberOfEnemiesToSpawn = enemySpawnPositions.Length;
        var enemiesSpawned = 0;
        while (enemiesSpawned < numberOfEnemiesToSpawn)
        {
            var positionIndex = Random.Range(0, tmpEnemySpawnPositions.Count);
            var spawnPosition = tmpEnemySpawnPositions[positionIndex].transform.position;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            tmpEnemySpawnPositions.RemoveAt(positionIndex);
            enemiesSpawned++;
        }
    }

    private IEnumerator DayTimer()
    {
        while (_currentTime <= _dayDuration)
        {
            _currentTime += Time.deltaTime;
            yield return null;
        }
        // TODO: Game Over
        Debug.Log("Current day ended");
    }
    
    public void OnGiftCollected()
    {
        _giftsCollected++;
        
        numberOfEnemiesToSpawn = (int)(Math.Log(numberOfEnemiesToSpawn) / Math.Log(2));
        // TODO: empowerment
        SpawnEnemies();

        if (_giftsCollected == _totalGifts)
        {
            // TODO: Spawn Boss wave
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
        // TODO: Update UI
        StopCoroutine(_dayTimerCoroutine);
        throw new NotImplementedException("OnPlayerDeath event not implemented yet.");
        Time.timeScale = 0; // Freeze the game
    }

    public void OnLevelCompleted()
    {
        // TODO: Play Win audio
        StopCoroutine(_dayTimerCoroutine);
        throw new NotImplementedException("OnLevelCompleted event not implemented yet.");
        Time.timeScale = 0; // Freeze the game
    }
}