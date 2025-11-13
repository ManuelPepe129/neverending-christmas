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

    [SerializeField] private GameObject[] giftSpawnPositions;
    [SerializeField] private GameObject giftToSpawn;
    [SerializeField] private int numberOfGiftsToSpawn;
    
    private int _totalGifts = 0;
    private int _giftsCollected = 0;

    private void Start()
    {
        // TODO: Move to UI
        StartGame();
    }

    void StartGame()
    {
        // Start timer
        _dayDuration = dayDuration * 60f; // Convert minutes to seconds
        _currentTime = 0f;
        _dayTimerCoroutine = StartCoroutine(DayTimer());
        
        // Spawn gifts
        var tmpGiftSpawnPositions = new List<GameObject>(giftSpawnPositions);
        if (numberOfGiftsToSpawn > giftSpawnPositions.Length) numberOfGiftsToSpawn = giftSpawnPositions.Length;
        int giftSpawned = 0;
        while (giftSpawned < numberOfGiftsToSpawn)
        {
            var positionIndex = Random.Range(0, tmpGiftSpawnPositions.Count);
            var spawnPosition = tmpGiftSpawnPositions[positionIndex].transform.position;
            Instantiate(giftToSpawn, spawnPosition, Quaternion.identity);
            tmpGiftSpawnPositions.RemoveAt(positionIndex);
            giftSpawned++;
        }
        
        // Start Waves
    }

    private IEnumerator DayTimer()
    {
        while (_currentTime <= _dayDuration)
        {
            _currentTime += Time.deltaTime;
            yield return new WaitForSeconds(1.0f);
        }
        // TODO: Game Over
        Debug.Log("Current day ended");
    }
    
    public void OnGiftCollected()
    {
        _giftsCollected++;

        if (_giftsCollected == _totalGifts)
        {
            // TODO: Spawn Boss wave
        }
    }

    private void SpawnBossWave()
    {
        throw new NotImplementedException("SpawnBossWave not implemented yet.");
    }
    
    public void OnPlayerDeath(bool isDeath)
    {
        // Play Game Over audio
        // Update UI
        StopCoroutine(_dayTimerCoroutine);
        throw new NotImplementedException("OnPlayerDeath event not implemented yet.");
        Time.timeScale = 0; // Freeze the game
    }

    public void OnLevelCompleted()
    {
        // Play Win audio
        StopCoroutine(_dayTimerCoroutine);
        throw new NotImplementedException("OnLevelCompleted event not implemented yet.");
        Time.timeScale = 0; // Freeze the game
    }
}